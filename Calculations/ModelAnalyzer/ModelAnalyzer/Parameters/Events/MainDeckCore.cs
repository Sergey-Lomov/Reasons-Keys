using System;
using System.Collections.Generic;
using System.Linq;

using ModelAnalyzer.DataModels;
using ModelAnalyzer.Services;
using ModelAnalyzer.Services.FieldAnalyzer;
using ModelAnalyzer.Parameters.Topology;
using ModelAnalyzer.Parameters.Timing;

namespace ModelAnalyzer.Parameters.Events
{
    class MainDeckCore : DeckParameter
    {
        public MainDeckCore()
        {
            type = ParameterType.Inner;
            title = "Ядро колоды событий континуума";
            details = "";
            tags.Add(ParameterTag.events);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            deck = InitialDeckWithRelationTemplates(calculator);

            if (!calculationReport.IsSuccess)
                return calculationReport;

            try
            {
                SetRelationsTypes(deck, calculator);
                PairReasons(deck, calculator);
                UpdateDeckWeight(calculator);
                AddStabilityBonuses(deck, calculator);
                UpdateDeckWeight(calculator);
                AddMiningBonuses(deck, calculator);
                UpdateDeckWeight(calculator);  
            }
            catch (MACalculationException)
            {
                deck = new List<EventCard>();
            }

            return calculationReport;
        }

        private List<EventCard> InitialDeckWithRelationTemplates(Calculator calculator)
        {
            var rtu = RequestParmeter<RelationTemplatesUsage>(calculator).GetNoZero();

            if (!calculationReport.IsSuccess)
                return new List<EventCard>();

            deck = new List<EventCard>();

            foreach (var template in rtu.Keys)
            {
                for (int i = 0; i < rtu[template].cardsCount; i++)
                {
                    var card = new EventCard();
                    card.relations = template.instantiateByReasons();
                    card.usability = rtu[template].usability;
                    deck.Add(card);
                }
            }

            return deck;
        }

        private void SetRelationsTypes(List<EventCard> deck, Calculator calculator)
        {
            var fbc = RequestParmeter<FrontBlockerCoefficient>(calculator).GetValue();
            var bbc = RequestParmeter<BackBlockerCoefficient>(calculator).GetValue();

            if (!calculationReport.IsSuccess)
                return;

            SetRelationsTypes(RelationDirection.back, bbc);
            SetRelationsTypes(RelationDirection.front, fbc);
        }

        private void SetRelationsTypes(RelationDirection direction, float blockerChance)
        {
            Func<EventCard, RelationDirection, int> dirAmount = (e, d) => e.relations.Where(r => r.direction == d).Count();
            var grouping = deck.GroupBy(e => dirAmount(e, direction)).Where( g => g.Key != 0);

            var types = new List<RelationType> { RelationType.reason, RelationType.blocker };
            var typesChances = new Dictionary<RelationType, float>
            {
                {RelationType.blocker, blockerChance},
                {RelationType.reason, 1 - blockerChance},
            };

            foreach (var group in grouping)
            {
                var cards = group.ToList();
                var combinations = MathAdditional.combinations(types, group.Key);
                var chances = combinations.Select(c => MathAdditional.combinationChance(c, typesChances)).ToList();
                var amounts = MathAdditional.AmountsForAllocation(group.Count(), chances, calculationReport).ToList();
                Action<EventCard, List<RelationType>> combinationSetter = 
                    (e, c) => ApplyRealtionsTypesCombination(e, direction, c);
                Setter.EvenDistributionSet(cards, combinations, amounts, combinationSetter);
            }
        }

        private void ApplyRealtionsTypesCombination(
            EventCard card,
            RelationDirection handleDirection,
            List<RelationType> combination)
        {
            var handlable = card.relations.Where(r => r.direction == handleDirection).ToList();
            if (handlable.Count() != combination.Count())
                return;

            for (int i = 0; i < combination.Count(); i++)
            {
                handlable[i].type = combination[i];
            }
        }

        private void PairReasons(List<EventCard> deck, Calculator calculator)
        {
            float pc = RequestParmeter<PairingCoef>(calculator).GetValue();
            var filtered = deck.Where(c => c.hasBackReason()).OrderBy(c => c.weight).ToList();
            int pairedCount = (int)Math.Round(deck.Count() * pc, MidpointRounding.AwayFromZero);
            int step = (int)Math.Floor(filtered.Count() / (float)pairedCount);
            step = step < 1 ? 1 : step;
            int index = step;
            while (index < filtered.Count())
            {
                filtered[index].isPairedReasons = true;
                index += step;
            }
        }

        private void AddStabilityBonuses(List<EventCard> cards, Calculator calculator)
        {
            float cna = RequestParmeter<ContinuumNodesAmount>(calculator).GetValue();
            List<float> sb_allocation = RequestParmeter<StabilityBonusAllocation>(calculator).GetValue();

            int[] sb_amounts = MathAdditional.AmountsForAllocation(cna, sb_allocation, calculationReport);
            if (!calculationReport.IsSuccess) return;

            var ordered = cards.OrderBy(c => c.weight).Reverse().ToList();
            var groups = SplitForAmounts(ordered, sb_amounts);

            for (int bonusIter = 0; bonusIter < groups.Count() ; bonusIter++)
            {
                foreach (EventCard card in groups[bonusIter])
                {
                    int index = groups[bonusIter].IndexOf(card);
                    int stabilityBonus = SpreadValue(bonusIter, index, sb_amounts);
                    card.stabilityBonus = stabilityBonus;
                    sb_amounts[stabilityBonus]--;
                }
            }
        }

        private void AddMiningBonuses(List<EventCard> cards, Calculator calculator)
        {
            float cna = RequestParmeter<ContinuumNodesAmount>(calculator).GetValue();
            List<float> mb_allocation = RequestParmeter<EventMiningBonusAllocation>(calculator).GetValue();

            int[] mb_amounts = MathAdditional.AmountsForAllocation(cna, mb_allocation, calculationReport);
            if (!calculationReport.IsSuccess) return;

            var ordered = cards.OrderBy(c => c.weight).Reverse().ToList();
            var groups = SplitForAmounts(ordered, mb_amounts);

            for (int bonusIter = 0; bonusIter < groups.Count(); bonusIter++)
            {
                foreach (EventCard card in groups[bonusIter])
                {
                    int index = groups[bonusIter].IndexOf(card);
                    int miningBonus = SpreadValue(bonusIter, index, mb_amounts);
                    card.miningBonus = miningBonus;
                    mb_amounts[miningBonus]--;
                }
            }
        }

        private Dictionary<int, List<EventCard>> SplitForAmounts (List<EventCard> cards, int[] amounts)
        {
            var groups = new Dictionary<int, List<EventCard>>();
            int cardIter = 0;
            for (int i = 0; i < amounts.Count(); i++)
            {
                groups.Add(i, new List<EventCard>());
                for (int j = 0; j < amounts[i]; j++)
                    groups[i].Add(cards[cardIter + j]);

                cardIter += amounts[i];
            }

            return groups;
        }

        private int SpreadValue(int defaultValue, int index, int[] amounts)
        {
            switch (index % 3)
            {
                case 0:
                    if (amounts[defaultValue] > 0)
                        return defaultValue;
                    else
                        return NearestAvailableValue(defaultValue, index % 2 == 0, amounts);
                case 1:
                    return NearestAvailableValue(defaultValue, false, amounts);
                case 2:
                    return NearestAvailableValue(defaultValue, true, amounts);
            }

            return 0;
        }

        private int NearestAvailableValue(int current, bool nextFirst, int[] amounts)
        {
            List<int> sequence = new List<int>();

            for (int i = 1; i < amounts.Count(); i++)
            {
                int rightIndex = current + i;
                int leftIndex = current - i;

                if (nextFirst)
                {
                    if (rightIndex < amounts.Count())
                        sequence.Add(rightIndex);
                    if (leftIndex >= 0)
                        sequence.Add(leftIndex);
                }
                else
                {
                    if (leftIndex >= 0)
                        sequence.Add(leftIndex);
                    if (rightIndex < amounts.Count())
                        sequence.Add(rightIndex);
                }
            }

            foreach (int value in sequence)
                if (amounts[value] > 0)
                    return value;

            return 0;
        }
    }
}