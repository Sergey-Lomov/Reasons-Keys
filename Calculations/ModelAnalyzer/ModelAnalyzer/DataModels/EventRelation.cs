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

        public static EventRelation BackReason(int position)
        {
            return new EventRelation(RelationType.reason, RelationDirection.back, position);
        }

        public static EventRelation FrontReason(int position)
        {
            return new EventRelation(RelationType.reason, RelationDirection.front, position);
        }

        public static EventRelation BackBlocker(int position)
        {
            return new EventRelation(RelationType.blocker, RelationDirection.back, position);
        }

        public static EventRelation FrontBlocker(int position)
        {
            return new EventRelation(RelationType.blocker, RelationDirection.front, position);
        }

        public static EventRelation Undefined(int position)
        {
            return new EventRelation(RelationType.undefined, RelationDirection.none, position);
        }

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
