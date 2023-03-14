using System;
using System.Collections.Generic;
using System.Linq;

using ModelAnalyzer.Services;
using ModelAnalyzer.DataModels;
using ModelAnalyzer.Parameters.General;
using ModelAnalyzer.Parameters.Activities;
using ModelAnalyzer.Parameters.Topology;
using System.Threading;

namespace ModelAnalyzer.Parameters.Events
{
    class MainDeck : DeckParameter
    {
        private readonly string artifactsIssue = "Невозможно распределить артефакты симметрично очкам ветвей.";
        private readonly string mbIssue = "Невозможно распределить бонусы добычи симметрично очкам ветвей.";
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

            var core = RequestParameter<MainDeckCore>(calculator);

            if (!calculationReport.IsSuccess)
                return calculationReport;

            deck = new List<EventCard>();
            foreach (var card in core.deck)
                deck.Add(new EventCard(card));

            AddBranchPoints(deck, calculator, true);
            UpdateDeckWeight(calculator);
            AddMiningBonuses(deck, calculator, calculationReport);
            UpdateDeckWeight(calculator);
            AddArtifacts(deck, calculator, calculationReport);
            // For now cards have no contraints
            //UpdateDeckConstraints(calculator);
            UpdateDeckWeight(calculator);
            UpdateDeckNames();

            return calculationReport;
        }

        private void AddArtifacts(List<EventCard> cards, Calculator calculator, ParameterCalculationReport report)
        {
            float ar = RequestParameter<ArtifactsRarity>(calculator).GetValue();
            int maxpa = (int)RequestParameter<MaxPlayersAmount>(calculator).GetValue();

            int amount = (int)Math.Round(cards.Count() * ar, MidpointRounding.AwayFromZero);
            var ordered = cards.OrderBy(c => c.weight);
            var filtered = ordered.Where(c => !c.branchPoints.HasPositivePoints()).ToList();
            AllocateSymmetrically(filtered, amount, maxpa, artifactsIssue, report, c => c.provideArtifact = true);
        }

        private void AllocateSymmetrically(
            List<EventCard> cards,
            int amount,
            int maxpa,
            string unallocatableIssue,
            ParameterCalculationReport report,
            Action<EventCard> applicator)
        {
            int doneAmount = 0;
            while (cards.Count() > 0 && doneAmount < amount)
            {
                var candidate = cards.First();
                var cartage = EventCardsAnalizer.FirstFullCartage(candidate, cards, maxpa);
                if (cartage == null)
                {
                    cards.Remove(candidate);
                    continue;
                }

                if (cartage.Count() + doneAmount <= amount)
                {
                    foreach (var card in cartage)
                        applicator(card);
                    doneAmount += cartage.Count();
                }

                cards = cards.Except(cartage).ToList();
            }

            if (doneAmount < amount)
                report.AddIssue(unallocatableIssue);
        }

        private void AddBranchPoints(List<EventCard> cards, Calculator calculator, bool threading)
        {
            if (threading)
                AddBranchPointsMultyThread(cards, calculator);
            else
                AddBranchPointsSingleThread(cards, calculator);
        } 

        private void AddBranchPointsMultyThread(List<EventCard> cards, Calculator calculator)
        {
            // Randomization
            var maxpa = (int)RequestParameter<MaxPlayersAmount>(calculator).GetValue();
            var bprl = (int)RequestParameter<BranchPointsRandomizationLimit>(calculator).GetValue();
            var bprs = (int)RequestParameter<BranchPointsRandomizationOffset>(calculator).GetValue();
            var aripc = RequestParameter<AverageRelationsImpactPerCount>(calculator).GetValue();
            var endless = RequestParameter<BranchPointsEndlessRandomization>(calculator).GetValue();

            var cardsStabilities = EventCardsAnalizer.CardsStabilities(deck, aripc);

            var randomizer = new Random(bprs);
            var bpSets = BranchPointSets(calculator);
            float minDeviation = float.MaxValue;
            var minDeviationBPSets = bpSets.ToList();

            var findValid = false;
            long iteration = 0;

            var countdown = new CountdownEvent(bprl);
            var findValidEvent = new ManualResetEvent(false);

            while ((iteration < bprl && !endless) || (endless && !findValid))
            {
                iteration++;
                var randomizedSets = bpSets.OrderBy(bps => randomizer.Next()).ToList();
                ThreadPool.QueueUserWorkItem(
                    _ =>
                    {
                        var deviation = EventCardsAnalizer.BrancheDisbalance(deck, cardsStabilities, maxpa, randomizedSets);
                        lock (this)
                        {
                            if (deviation < minDeviation)
                            {
                                if (deviation < BranchPointsDisbalance.criticalValue)
                                {
                                    findValid = true;
                                    if (endless) 
                                        findValidEvent.Set();
                                }
                                
                                minDeviation = deviation;
                                minDeviationBPSets = randomizedSets;
                            }
                        }

                        if (!endless)
                        {
                            countdown.Signal();
                        }
                    });
            }

            if (endless)
            {
                findValidEvent.WaitOne();
            } else
            {
                countdown.Wait();
            }

            var sets = bpSets.OrderBy(bps => randomizer.Next()).ToArray();
            for (int i = 0; i < cards.Count(); i++)
                cards[i].branchPoints = minDeviationBPSets[i];
        }

        private void AddBranchPointsSingleThread(List<EventCard> cards, Calculator calculator)
        {
            // Randomization
            var maxpa = (int)RequestParameter<MaxPlayersAmount>(calculator).GetValue();
            var bprl = (int)RequestParameter<BranchPointsRandomizationLimit>(calculator).GetValue();
            var aripc = RequestParameter<AverageRelationsImpactPerCount>(calculator).GetValue();

            var cardsStabilities = EventCardsAnalizer.CardsStabilities(deck, aripc);

            var randomizer = new Random(0);
            var bpSets = BranchPointSets(calculator);
            float minDeviation = float.MaxValue;
            var minDeviationBPSets = bpSets.ToList();

            for (long i = 0; i < bprl; i++)
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
            float cna = RequestParameter<ContinuumNodesAmount>(calculator).GetValue();
            List<float> bpta = RequestParameter<BrachPointsTemplatesAllocation>(calculator).GetValue();

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
            float mpa = RequestParameter<MaxPlayersAmount>(calculator).GetValue();

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
            var bpa_std = (BranchPointsAllocation)RequestParameter<BranchPointsAllocation_Standard>(calculator);
            var bpa_sym = (BranchPointsAllocation)RequestParameter<BranchPointsAllocation_Symmetric>(calculator);

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

        private void AddMiningBonuses(List<EventCard> cards, Calculator calculator, ParameterCalculationReport report)
        {
            float cna = RequestParameter<ContinuumNodesAmount>(calculator).GetValue();
            int maxpa = (int)RequestParameter<MaxPlayersAmount>(calculator).GetValue();
            List<float> mb_allocation = RequestParameter<EventMiningBonusAllocation>(calculator).GetValue();

            int[] mb_amounts = MathAdditional.AmountsForAllocation(cna, mb_allocation, calculationReport);
            if (!calculationReport.IsSuccess) return;

            var ordered = cards.OrderBy(c => c.weight).ToList();

            for (int i = 1; i < mb_amounts.Length; i++)
            {
                int amount = mb_amounts[i];
                if (amount == 0) continue;
                AllocateSymmetrically(ordered, amount, maxpa, mbIssue, report, c => c.miningBonus = i);
                ordered = cards.Where(c => c.miningBonus == 0).ToList();
            }
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