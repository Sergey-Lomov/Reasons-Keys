using System;
using System.Linq;
using System.Collections.Generic;

namespace ModelAnalyzer.DataModels
{
    using EventRelations = List<EventRelation>;

    enum RelationDirection { back, front }
    enum RelationType { reason, blocker, paired_reason }

    internal class EventRelation : IEquatable<EventRelation>
    {
        internal const int MaxRelationPosition = 6;

        internal RelationType type;
        internal RelationDirection direction;
        internal int position;

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

    class EventCard : IEquatable<EventCard>
    {
        internal EventRelations relations = new EventRelations();
        internal int stabilityIncrement = 0;
        internal int miningBonus = 0;
        internal bool provideArtifact = false;
        internal bool isKey = false;
        internal BranchPointsSet branchPoints = new BranchPointsSet(null, null);
        internal int minRadisuConstrint = 0;
        internal int minPhaseConstraint = 0;

        internal float weight = 0;
        internal float usability = 0;
        internal string comment = "";

        public bool Equals(EventCard other)
        {
            return relations.SequenceEqual(other.relations)
                && stabilityIncrement == other.stabilityIncrement
                && miningBonus == other.miningBonus
                && provideArtifact == other.provideArtifact
                && isKey == other.isKey
                && branchPoints == other.branchPoints
                && minRadisuConstrint == other.minRadisuConstrint
                && minPhaseConstraint == other.minPhaseConstraint;
        }
    }
}
