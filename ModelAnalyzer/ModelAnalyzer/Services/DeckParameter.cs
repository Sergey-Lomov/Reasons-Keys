using System.Collections.Generic;

using ModelAnalyzer.DataModels;
using ModelAnalyzer.Services;

namespace ModelAnalyzer
{
    abstract class DeckParameter : Parameter
    {
        const string valueStub = "Колода";
        internal List<EventCard> deck = new List<EventCard>();

        public override void SetupByString(string str)
        {
            // Not possible. This parameter should be calculated.
        }

        public override string StringRepresentation()
        {
            return valueStub;
        }

        public override string UnroundValueToString()
        {
            return valueStub;
        }

        public override string ValueToString()
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
