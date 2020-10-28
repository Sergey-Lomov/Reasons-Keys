using System.Collections.Generic;
using ModelAnalyzer.DataModels;

namespace ModelAnalyzer.Services
{
    using EventRelations = List<EventRelation>;

    public static class EventRelationsFactory
    {
        // Build relations by patterns. Described at https://docs.google.com/document/d/1F8fQqkPXiYDJIPt2FnxM0RVCAk_1swKBiGtGoSUpTiY/edit?usp=sharing

        private static EventRelations CPattern(RelationType frontType, RelationType backType, int fronPosition, int backPosition)
        {
            var front = new EventRelation(frontType, RelationDirection.front, fronPosition);
            var back = new EventRelation(backType, RelationDirection.back, backPosition);
            return new EventRelations { front, back };
        }

        internal static EventRelations C0(RelationType frontType, RelationType backType) 
        {
            return CPattern(frontType, backType, 5, 0);
        }

        internal static EventRelations C1(RelationType frontType, RelationType backType)
        {
            return CPattern(frontType, backType, 4, 0);
        }

        internal static EventRelations C2(RelationType frontType, RelationType backType)
        {
            return CPattern(frontType, backType, 4, 1);
        }

        internal static EventRelations C3(RelationType frontType, RelationType backType)
        {
            return CPattern(frontType, backType, 4, 2);
        }

        internal static EventRelations C4(RelationType frontType, RelationType backType)
        {
            return CPattern(frontType, backType, 3, 2);
        }
    }
}
