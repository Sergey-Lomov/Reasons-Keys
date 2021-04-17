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
                SetRelationsTypes(deck);
                UpdateDeckWeight(calculator);
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

        private void SetRelationsTypes(List<EventCard> deck)
        {
            var randomizer = new Random(0);
            var randomised = deck.OrderBy(bps => randomizer.Next()).ToList();
            SetRelationsTypes(randomised, RelationDirection.back, 0.5f);
            SetRelationsTypes(randomised, RelationDirection.front, 0.5f);
        }

        private void SetRelationsTypes(List<EventCard> deck, RelationDirection direction, float blockerChance)
        {
            int dirAmount(EventCard e, RelationDirection d) => e.relations.Where(r => r.direction == d).Count();
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
                void combinationSetter(EventCard e, List<RelationType> c) => ApplyRealtionsTypesCombination(e, direction, c);
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
    }
}