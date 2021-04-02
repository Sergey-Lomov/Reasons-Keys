using System;
using System.Collections.Generic;
using System.Linq;

using ModelAnalyzer.Services;
using ModelAnalyzer.DataModels;
using ModelAnalyzer.Parameters.General;
using ModelAnalyzer.Parameters.Activities;
using ModelAnalyzer.Parameters.Topology;

namespace ModelAnalyzer.Parameters.Events
{
    class MainDeck : DeckParameter
    {
        private readonly string artifactsIssue = "Невозможно распределить артефакты симметрично очкам ветвей.";
        private readonly string cardsNamesPrefix = "Main";

        public MainDeck()
        {
            type = ParameterType.Out;
            title = "Колода событий континуума";
            details = "";
            tags.Add(ParameterTag.events);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            var core = RequestParmeter<MainDeckCore>(calculator);

            if (!calculationReport.IsSuccess)
                return calculationReport;

            deck = new List<EventCard>();
            foreach (var card in core.deck)
                deck.Add(new EventCard(card));

            AddBranchPoints(deck, calculator);
            AddArtifacts(deck, calculator, calculationReport);
            UpdateDeckConstraints(calculator);
            UpdateDeckWeight(calculator);
            UpdateDeckNames();

            return calculationReport;
        }

        private void AddArtifacts(List<EventCard> cards, Calculator calculator, ParameterCalculationReport report)
        {
            float ar = RequestParmeter<ArtifactsRarity>(calculator).GetValue();
            int maxpa = (int)RequestParmeter<MaxPlayersAmount>(calculator).GetValue();

            int estimatedAmount = (int)Math.Round(cards.Count() * ar, MidpointRounding.AwayFromZero);
            var ordered = cards.OrderBy(c => c.weight).ToList();

            int amount = 0;
            while (ordered.Count() > 0 && amount < estimatedAmount)
            {
                var candidate = ordered.First();
                if (candidate.branchPoints.HasPositivePoints())
                {
                    ordered.Remove(candidate);
                    continue;
                }

                var cartage = EventCardsAnalizer.FirstFullCartage(candidate, ordered, maxpa);
                if (cartage == null)
                {
                    ordered.Remove(candidate);
                    continue;
                }

                if (cartage.Count() + amount <= estimatedAmount)
                {
                    foreach (var card in cartage)
                        card.provideArtifact = true;
                    amount += cartage.Count();
                }

                ordered = ordered.Except(cartage).ToList();
            }

            if (amount < estimatedAmount)
                report.AddIssue(artifactsIssue);
        }

        private void AddBranchPoints(List<EventCard> cards, Calculator calculator)
        {
            // Randomization
            var maxpa = (int)RequestParmeter<MaxPlayersAmount>(calculator).GetValue();
            var bprl = (int)RequestParmeter<BranchPointsRandomizationLimit>(calculator).GetValue();
            var aripc = RequestParmeter<AverageRelationsImpactPerCount>(calculator).GetValue();

            var cardsStabilities = EventCardsAnalizer.CardsStabilities(deck, aripc);

            var randomizer = new Random(0);
            var bpSets = BranchPointSets(calculator);
            float minDeviation = float.MaxValue;
            var minDeviationBPSets = bpSets.ToList();
            for (int i = 0; i < bprl; i++)
            {
                var randomizedSets = bpSets.OrderBy(bps => randomizer.Next()).ToList();
                var deviation = EventCardsAnalizer.BrancheDisbalance(deck, cardsStabilities, maxpa, randomizedSets);
                if (deviation < minDeviation)
                {
                    minDeviation = deviation;
                    minDeviationBPSets = randomizedSets;
                }
            }

            var sets = bpSets.OrderBy(bps => randomizer.Next()).ToArray();
            for (int i = 0; i < cards.Count(); i++)
                cards[i].branchPoints = minDeviationBPSets[i];
        }

        private List<BranchPointsSet> BranchPointSets(Calculator calculator)
        {
            var bpSets = new List<BranchPointsSet>();
            var templatesAmount = AmountsForTemplates(calculator);
            foreach (var template in templatesAmount.Keys)
            {
                var sequence = SequenceForTemplate(template, templatesAmount[template], calculator);
                bpSets.AddRange(sequence);
            }

            return bpSets;
        }

        private Dictionary<BranchPointsTemplate, int> AmountsForTemplates(Calculator calculator)
        {
            float cna = RequestParmeter<ContinuumNodesAmount>(calculator).GetValue();
            List<float> bpta = RequestParmeter<BrachPointsTemplatesAllocation>(calculator).GetValue();

            var templates = new BrachPointsTemplatesAllocation().templates;
            int[] amounts = MathAdditional.AmountsForAllocation(cna, bpta, calculationReport);
            Dictionary<BranchPointsTemplate, int> templateAmount = new Dictionary<BranchPointsTemplate, int>();
            for (int i = 0; i < amounts.Length; i++)
            {
                var template = templates[i];
                templateAmount[template] = amounts[i];
            }

            return templateAmount;
        }

        private List<BranchPointsSet> SequenceForTemplate(BranchPointsTemplate template, int lenght, Calculator calculator)
        {
            var sequence = new List<BranchPointsSet>();
            int branchesAmount = template.failed.Count() + template.success.Count();
            switch (branchesAmount)
            {
                case 0:
                    sequence = EmptySetsSequence(lenght);
                    break;

                case 1:
                    sequence = SingleBranchSets(template, lenght, calculator);
                    break;

                case 2:
                    sequence = DoubleBranchSequence(template, lenght, calculator);
                    break;
            }

            return sequence;
        }

        private List<BranchPointsSet> EmptySetsSequence(int lenght)
        {
            var sequence = new List<BranchPointsSet>();
            for (int i = 0; i < lenght; i++)
                sequence.Add(new BranchPointsSet(null, null));

            return sequence;
        }

        private List<BranchPointsSet> SingleBranchSets(BranchPointsTemplate template, int lenght, Calculator calculator)
        {
            float mpa = RequestParmeter<MaxPlayersAmount>(calculator).GetValue();

            var sets = new List<BranchPointsSet>();
            int index = 0;
            for (int i = 0; i < lenght; i++)
            {
                var set = template.SetupByBranches(new int[] { index });
                sets.Add(set);
                index = index == mpa - 1 ? 0 : index + 1;
            }

            return sets;
        }

        private List<BranchPointsSet> DoubleBranchSequence(BranchPointsTemplate template, int lenght, Calculator calculator)
        {
            var bpa_std = (BranchPointsAllocation)RequestParmeter<BranchPointsAllocation_Standard>(calculator);
            var bpa_sym = (BranchPointsAllocation)RequestParmeter<BranchPointsAllocation_Symmetric>(calculator);

            if (!calculationReport.IsSuccess)
            {
                var exception = new MACalculationException("");
                throw exception;
            }

            var sequence = new List<BranchPointsSet>();
            var activeAllocation = bpa_std;
            int index = 0;
            for (int i = 0; i < lenght; i++)
            {
                var pair = activeAllocation.values[index];
                var branches = new int[] { pair.Item1, pair.Item2 };
                var set = template.SetupByBranches(branches);
                sequence.Add(set);

                index++;
                if (index == activeAllocation.values.Count())
                {
                    activeAllocation = activeAllocation == bpa_std ? bpa_sym : bpa_std;
                    index = 0;
                }
            }

            return sequence;
        }

        private void UpdateDeckNames()
        {
            for (int i = 0; i < deck.Count(); i++)
            {
                deck[i].name = cardsNamesPrefix + (i + 1);
            }
        }
    }
}