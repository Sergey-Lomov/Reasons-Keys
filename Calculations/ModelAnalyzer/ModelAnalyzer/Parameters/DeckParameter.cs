using System.Linq;
using System.Collections.Generic;

using ModelAnalyzer.DataModels;
using ModelAnalyzer.Services;

namespace ModelAnalyzer.Parameters
{
    abstract class DeckParameter : Parameter
    {
        const string valueStub = "Колода";
        internal List<EventCard> deck = null;

        internal override Parameter Copy()
        {
            var copy = base.Copy() as DeckParameter;

            if (deck != null)
                copy.deck = new List<EventCard>(deck);

            return copy;
        }

        internal override bool IsEqual(Parameter p)
        {
            if (!(p is DeckParameter))
                return false;

            var baseCheck = base.IsEqual(p);

            var dp = p as DeckParameter;
            if (deck == null || dp.deck == null)
                return deck == null && dp.deck == null;

            var deckCheck = dp.deck.SequenceEqual(deck);

            return baseCheck && deckCheck;
        }

        public override void SetupByString(string str)
        {
            // Not possible. This parameter should be calculated.
        }

        public override string StringRepresentation()
        {
            return valueStub;
        }

        public int RelationsAmount(RelationType type, RelationDirection direction)
        {
            bool isValid(EventRelation r) => r.type == type && r.direction == direction;
            int validInList(List<EventRelation> rl) => rl.Where(r => isValid(r)).Count();
            var relationsLists = deck.Select(c => c.relations);
            return relationsLists.Select(rl => validInList(rl)).Sum();
        }

        protected void UpdateDeckWeight(Calculator calculator)
        {
            foreach (EventCard card in deck)
                card.weight = EventCardsAnalizer.WeightForCard(card, calculator);
        }

        protected void UpdateDeckConstraints(Calculator calculator)
        {
            foreach (EventCard card in deck)
                EventCardsAnalizer.UpdateCardConstraints(card, calculator);
        }


        protected void UpdateDeckUsability(Calculator calculator)
        {
            foreach (EventCard card in deck)
                card.usability = EventCardsAnalizer.RelationsUsability(card.relations, calculator);
        }

        public override bool isValueNull()
        {
            return deck == null;
        }

        protected override void NullifyValue()
        {
            deck = null;
        }

        internal override bool VerifyValue()
        {
            bool baseVerify = base.VerifyValue();

            return baseVerify && deck != null;
        }
    }
}
