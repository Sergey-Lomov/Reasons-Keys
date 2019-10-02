using System.Linq;
using System.Collections.Generic;

using ModelAnalyzer.DataModels;
using ModelAnalyzer.Services;

namespace ModelAnalyzer.Parameters
{
    abstract class DeckParameter : Parameter
    {
        const string valueStub = "Колода";
        internal List<EventCard> deck = new List<EventCard>();

        internal override Parameter Copy()
        {
            var copy = base.Copy() as DeckParameter;

            copy.deck.AddRange(deck);

            return copy;
        }

        internal override bool IsEqual(Parameter p)
        {
            if (!(p is DeckParameter))
                return false;

            var baseCheck = base.IsEqual(p);

            var fsp = p as DeckParameter;
            var deckCheck = fsp.deck.SequenceEqual(deck);

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

        protected void UpdateDeckWeight(Calculator calculator)
        {
            foreach (EventCard card in deck)
                card.weight = EventCardsAnalizer.WeightForCard(card, calculator);
        }

        protected void UpdateDeckUsability(Calculator calculator)
        {
            foreach (EventCard card in deck)
                card.usability = EventCardsAnalizer.RelationsUsability(card.relations, calculator);
        }
    }
}
