﻿using System.Collections.Generic;

namespace ModelAnalyzer.DataModels
{
    using EventRelations = List<EventRelation>;

    enum RelationDirection { back, front }
    enum RelationType { reason, blocker, paired_reason }

    internal class EventRelation
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
    }

    class EventCard
    {
        internal EventRelations relations = new EventRelations();
        internal int stabilityIncrement = 0;
        internal int miningBonus = 0;
        internal bool provideArtifact = false;
        internal BranchPointsSet branchPoints;
        internal (int branch, int bonus) succesBranchPoints = (0, 0);
        internal (int branch, int bonus) failedBranchPoints = (0, 0);
        internal int minRadisuConstrint = 0;
        internal int minPhaseConstraint = 0;

        internal float weight = 0;
        internal float usability = 0;
    }
}
