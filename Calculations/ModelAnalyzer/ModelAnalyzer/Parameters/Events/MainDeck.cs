using System;
using System.Collections.Generic;
using System.Linq;

using ModelAnalyzer.Services;
using ModelAnalyzer.DataModels;
using ModelAnalyzer.Parameters.General;
using ModelAnalyzer.Parameters.Topology;

namespace ModelAnalyzer.Parameters.Events
{
    class MainDeck : DeckParameter
    {
        private Random randomizer;
        private int randomizerSeed = 485316097;

        private readonly string roundingIssue = "Невозможно корректно округлить значения при распределении. Сумма округленных значений отличется суммы не округленных.";

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
            randomizer = new Random(randomizerSeed);

            var core = RequestParmeter<MainDeckCore>(calculator);

            if (!calculationReport.IsSuccess)
                return calculationReport;

            deck = new List<EventCard>();
            foreach (var card in core.deck)
                deck.Add(new EventCard(card));

            AddBranchPoints(deck, calculator);
            AddArtifacts(deck, calculator);
            UpdateDeckConstraints(calculator);
            UpdateDeckWeight(calculator);

            return calculationReport;
        }

        private void AddArtifacts(List<EventCard> cards, Calculator calculator)
        {
            float ar = RequestParmeter<ArtifactsRarity>(calculator).GetValue();
            float cna = RequestParmeter<ContinuumNodesAmount>(calculator).GetValue();

            int amount = (int)Math.Round(cna * ar, MidpointRounding.AwayFromZero);
            var ordered = cards.OrderBy(c => c.weight).ToList();
            var step = (int)Math.Floor(cards.Count() / 2f / amount);

            for (int i = 0; i < amount; i++)
                ordered[i * step].provideArtifact = true;
        }

        private void AddBranchPoints(List<EventCard> cards, Calculator calculator)
        {
            var randomizedCards = cards.OrderBy(c => randomizer.Next()).ToList();
            var templateCards = SplitCardsForTemplates(randomizedCards, calculator);
            foreach (var template in templateCards.Keys)
            {
                var tCards = templateCards[template];
                var sequence = SequenceForTemplate(template, tCards.Count(), calculator);

                foreach (var card in tCards)
                {
                    var setIndex = randomizer.Next(sequence.Count);
                    var set = sequence[setIndex];
                    card.branchPoints = set;
                    sequence.Remove(set);
                }
            }
        }

        private Dictionary<BranchPointsTemplate, List<EventCard>> SplitCardsForTemplates(List<EventCard> cards, Calculator calculator)
        {
            float cna = RequestParmeter<ContinuumNodesAmount>(calculator).GetValue();
            List<float> bpta = RequestParmeter<BrachPointsTemplatesAllocation>(calculator).GetValue();

            var templates = new BrachPointsTemplatesAllocation().templates;
            int[] amounts = AmountsForAllocation(cna, bpta);
            Dictionary<BranchPointsTemplate, int> templateAmount = new Dictionary<BranchPointsTemplate, int>();
            for (int i = 0; i < amounts.Length; i++)
            {
                var template = templates[i];
                templateAmount[template] = amounts[i];
            }

            Dictionary<BranchPointsTemplate, List<EventCard>> templateCards = new Dictionary<BranchPointsTemplate, List<EventCard>>();
            foreach (var template in templates)
                templateCards[template] = new List<EventCard>();

            var templatesWithAmounts = templates.Where(t => templateAmount.ContainsKey(t));
            var uncompletedTemplates = templatesWithAmounts.Where(t => templateAmount[t] != 0).ToList();

            foreach (var card in cards)
            {
                var templateIndex = randomizer.Next(uncompletedTemplates.Count);
                var template = uncompletedTemplates[templateIndex];
                templateCards[template].Add(card);

                if (templateCards[template].Count == templateAmount[template])
                    uncompletedTemplates.Remove(template);
            }

            return templateCards;
        }

        private int[] AmountsForAllocation(float cna, List<float> allocation)
        {
            int[] amounts = new int[allocation.Count()];
            for (int i = 0; i < allocation.Count(); i++)
            {
                var amount = cna * allocation[i] / allocation.Sum();
                amounts[i] = (int)Math.Round(amount, MidpointRounding.AwayFromZero);
            }

            if (amounts.Sum() != cna)
            {
                calculationReport.Failed(roundingIssue);
                return null;
            }

            return amounts;
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
    }
}
