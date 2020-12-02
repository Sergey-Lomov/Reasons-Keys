using System;

using ModelAnalyzer.Services.FieldAnalyzer;

namespace ModelAnalyzer.DataModels
{
    internal class EventRelation : IEquatable<EventRelation>
    {
        internal const int MaxRelationPosition = Field.nearesNodesAmount;

        internal RelationType type;
        internal RelationDirection direction;
        internal int position; // 0 - bottom and another clockwise

        public EventRelation(RelationType type, RelationDirection direction, int position)
        {
            this.type = type;
            this.direction = direction;
            this.position = position;
        }

        public bool Equals(EventRelation other)
        {
            return type == other.type
                && direction == other.direction
                && position == other.position;
        }
    }
}
