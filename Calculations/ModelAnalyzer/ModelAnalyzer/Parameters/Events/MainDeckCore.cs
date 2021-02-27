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
        private readonly string roundingIssue = "Невозможно корректно округлить значения при распределении. Сумма округленных значений отличется суммы не округленных.";

        readonly int sidesAmount = Field.nearesNodesAmount;

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
            var na = (int)RequestParmeter<ContinuumNodesAmount>(calculator).GetValue();
            var pd = RequestParmeter<PhasesDuration>(calculator).GetValue();
            var fr = (int)RequestParmeter<FieldRadius>(calculator).GetValue();
            var rna = RequestParmeter<RoundNodesAmount>(calculator).GetValue();
            var fec = RequestParmeter<FrontEventsCoef>(calculator).GetValue();
            var mrtu = RequestParmeter<MinRelationsTemplateUsability>(calculator).GetValue();
            var emr = (int)RequestParmeter<EventMaxRelations>(calculator).GetValue();
            var mbr = (int)RequestParmeter<MinBackRelations>(calculator).GetValue();

            if (!calculationReport.IsSuccess)
                return new List<EventCard>();

            var fieldAnalyzer = new FieldAnalyzer(phasesCount: pd.Count);
            var intPd = pd.Select(v => (int)v).ToList();
            fieldAnalyzer.templateUsabilityPrecalculations(intPd, fr);
            Func<EventRelationsTemplate, float> usability = t => fieldAnalyzer.templateUsability(t, rna);

            deck = new List<EventCard>(na);
            var allDirectionsTemplates = EventRelationsTemplate.allTemplates(sidesAmount);
            var templatesUsability = allDirectionsTemplates.ToDictionary(t => t, t => usability(t));
            var filteredTemplates = templatesUsability.Where(kvp => kvp.Value >= mrtu).ToList();

            Func<EventRelationsTemplate, bool> minRelValid = t => t.directionsAmount() > 0;
            Func<EventRelationsTemplate, bool> maxRelValid = t => t.directionsAmount() <= emr;
            Func<EventRelationsTemplate, bool> minBackValid = t => t.backAmount() >= mbr;
            Func<EventRelationsTemplate, bool> validate = t => minRelValid(t) && maxRelValid(t) && minBackValid(t);
            filteredTemplates = filteredTemplates.Where(kvp => validate(kvp.Key)).ToList();
            var templates_2d = filteredTemplates.Where(p => p.Key.containsFront()).ToDictionary(p => p.Key, p => p.Value);
            var templates_ob = filteredTemplates.Where(p => !p.Key.containsFront()).ToDictionary(p => p.Key, p => p.Value);

            var cardsAmount_2d = (int)Math.Round(na * fec, MidpointRounding.AwayFromZero);
            var cards_2d = CardsForTemplatesUsabilities(calculator, cardsAmount_2d, templates_2d);
            int cardsAmount_ob = na - cardsAmount_2d;
            var cards_ob = CardsForTemplatesUsabilities(calculator, cardsAmount_ob, templates_ob);

            deck.AddRange(cards_2d);
            deck.AddRange(cards_ob);

            return deck;
        }

        private List<EventCard> CardsForTemplatesUsabilities(Calculator calculator,
            int cardsAmount,
            Dictionary<EventRelationsTemplate, float> templatesUsabilities)
        {
            var cards = new List<EventCard>();
            var ordered = templatesUsabilities.OrderByDescending(kvp => kvp.Value);
            var templates = ordered.Select(kvp => kvp.Key).ToList();
            var usabilities = ordered.Select(kvp => kvp.Value).ToList();
            var groupsAmounts = AmountsForAllocation(cardsAmount, usabilities);

            for (int groupIter = 0; groupIter < groupsAmounts.Count(); groupIter++)
            {
                for (int cardIter = 0; cardIter < groupsAmounts[groupIter]; cardIter++)
                {
                    var card = new EventCard();
                    card.relations = templates[groupIter].instantiateByReasons();
                    card.usability = usabilities[groupIter];
                    cards.Add(card);
                }
            }

            return cards;
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
                var amounts = AmountsForAllocation(group.Count(), chances).ToList();
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

            int[] sb_amounts = AmountsForAllocation(cna, sb_allocation);
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

            int[] mb_amounts = AmountsForAllocation(cna, mb_allocation);
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

        private int[] AmountsForAllocation (float totalAmount, List<float> allocation)
        {
            int[] amounts = new int[allocation.Count()];
            float roundCredit = 0;
            for (int i = 0; i < allocation.Count(); i++)
            {
                if (allocation[i] == 0)
                    continue;

                var amount = totalAmount * allocation[i] / allocation.Sum() + roundCredit;
                amounts[i] = (int)Math.Round(amount, MidpointRounding.AwayFromZero);
                roundCredit = amount - amounts[i];
            }

            if (amounts.Sum() != totalAmount)
            {
                calculationReport.Failed(roundingIssue);
                return new int[0];
            }

            return amounts;
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