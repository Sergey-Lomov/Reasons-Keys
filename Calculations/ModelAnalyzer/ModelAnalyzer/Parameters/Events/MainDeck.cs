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

            var core = RequestParmeter<MainDeckCore>(calculator);

            if (!calculationReport.IsSuccess)
                return calculationReport;

            deck = new List<EventCard>();
            foreach (var card in core.deck)
                deck.Add(new EventCard(card));

            AddPositiveRealisationChances(deck, calculator);
            AddBranchPoints(deck, calculator);
            AddArtifacts(deck, calculator);
            UpdateDeckConstraints(calculator);
            UpdateDeckWeight(calculator);

            return calculationReport;
        }

        private void AddPositiveRealisationChances(List<EventCard> cards, Calculator calculator)
        {
            var minpa = (int)RequestParmeter<MinPlayersAmount>(calculator).GetValue();
            var maxpa = (int)RequestParmeter<MaxPlayersAmount>(calculator).GetValue();
            var cna = RequestParmeter<ContinuumNodesAmount>(calculator).GetValue();
            var eca = calculator.UpdatedParameter<EventCreationAmount>().GetValue();
            var aprc = calculator.UpdatedParameter<AveragePositiveRealisationChance>().GetValue().Average();
            var bea = RequestParmeter<BackEdgesAmount>(calculator).GetValue();
            var bric = RequestParmeter<BackRelationIgnoringChance>(calculator).GetValue();
            var tfra = RequestParmeter<FrontReasonsInEstimatedDeck>(calculator).GetValue();
            var tfba = RequestParmeter<FrontBlockersInEstimatedDeck>(calculator).GetValue();
            var core = RequestParmeter<MainDeckCore>(calculator).deck;
            var startDeck = RequestParmeter<MainDeckCore>(calculator).deck;

            float ane = bea / cna;
            float edca(int pa) => core.Count() + startDeck.Count() * pa / maxpa;
            float calcAfra(int pa) => tfra[pa - minpa] / edca(pa) * pa * eca / bea * ane;
            float calcAfba(int pa) => tfba[pa - minpa] / edca(pa) * pa * eca / bea * ane;

            var afra = new List<float>();
            var afba = new List<float>();
            for (int pa = minpa; pa <= maxpa; pa++)
            {
                var afraValue = calcAfra(pa);
                var afbaValue = calcAfba(pa);
                afra.Add(afraValue);
                afba.Add(afbaValue);
            }

            double aafra = afra.Average();
            double aafba = afba.Average();
            double abric = bric.Average();

            foreach (var card in cards)
                card.positiveRealisationChance = (float)EventCardsAnalizer.PositiveRealisationChance(card, aprc, aafra, aafba, abric);
        }

        private void AddArtifacts(List<EventCard> cards, Calculator calculator)
        {
            float ar = RequestParmeter<ArtifactsRarity>(calculator).GetValue();
            int maxpa = (int)RequestParmeter<MaxPlayersAmount>(calculator).GetValue();

            int estimatedAmount = (int)Math.Round(cards.Count() * ar, MidpointRounding.AwayFromZero);
            var ordered = cards.OrderBy(c => c.weight).ToList();

            int amount = 0;
            while (ordered.Count() > 0 && amount < estimatedAmount)
            {
                var candidate = ordered.First();
                var cartage = EventCardsAnalizer.FirstFullCartage(candidate, ordered, maxpa);
                if (cartage == null) {
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
        }

        private void AddBranchPoints(List<EventCard> cards, Calculator calculator)
        {
            // Calculation
            /*          var maxpa = (int)RequestParmeter<MaxPlayersAmount>(calculator).GetValue();

                      List<float> signPoints = Enumerable.Repeat(0f, maxpa).ToList();
                      List<float> totalPoints = Enumerable.Repeat(0f, maxpa).ToList();
                      var bpSets = BranchPointSets(calculator);

                      double cardBalance(EventCard card) => Math.Abs(0.5 - card.positiveRealisationChance);
                      var sortedCards = cards.OrderBy(c => cardBalance(c)).Reverse().ToList();
                      foreach (var card in sortedCards)
                      {
                          float orderFunc(BranchPointsSet set) => EventCardsAnalizer.PointsByAppend(signPoints, card, set).Deviation();
                          var minDisbalanceSet = bpSets.OrderBy( s => orderFunc(s)).First();
                          signPoints = EventCardsAnalizer.PointsByAppend(signPoints, card, minDisbalanceSet);
                          totalPoints = EventCardsAnalizer.PointsByAppend(totalPoints, card, minDisbalanceSet, false);
                          card.branchPoints = minDisbalanceSet;
                          bpSets.Remove(minDisbalanceSet);
                      }*/

            // Randomization
            var maxpa = (int)RequestParmeter<MaxPlayersAmount>(calculator).GetValue();
            var bprl = (int)RequestParmeter<BranchPointsRandomizationLimit>(calculator).GetValue();

            var randomizer = new Random(0);
            var bpSets = BranchPointSets(calculator);
            float minDeviation = float.MaxValue;
            var minDeviationBPSets = bpSets.ToArray();
            for (int i = 0; i < bprl; i++)
            {
                List<float> points = Enumerable.Repeat(0f, maxpa).ToList();
                var sets = bpSets.OrderBy(bps => randomizer.Next()).ToArray();
                for (int j = 0; j < cards.Count(); j++)
                    points = EventCardsAnalizer.PointsByAppend(points, cards[j], sets[j]);
                var deviation = points.Deviation();
                if (deviation < minDeviation) {
                    minDeviation = deviation;
                    minDeviationBPSets = sets;
                }
            }

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
            int[] amounts = AmountsForAllocation(cna, bpta);
            Dictionary<BranchPointsTemplate, int> templateAmount = new Dictionary<BranchPointsTemplate, int>();
            for (int i = 0; i < amounts.Length; i++)
            {
                var template = templates[i];
                templateAmount[template] = amounts[i];
            }

            return templateAmount;
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
