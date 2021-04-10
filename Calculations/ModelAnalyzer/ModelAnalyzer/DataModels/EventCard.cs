using System;
using System.Linq;
using System.Collections.Generic;

namespace ModelAnalyzer.DataModels
{
    using EventRelations = List<EventRelation>;

    enum RelationDirection { back, front, none }
    enum RelationType { reason, blocker, undefined}

    internal class EventConstraints
    {
        internal List<int> unavailableRadiuses = new List<int>();
        internal int minPhase = 0;
        internal int minStability = 0;

        internal void MakeEqual (EventConstraints constraints)
        {
            unavailableRadiuses = new List<int>(constraints.unavailableRadiuses);
            minPhase = constraints.minPhase;
            minStability = constraints.minStability;
        }

        public bool Equals(EventConstraints constraints)
        {
            return unavailableRadiuses.SequenceEqual(constraints.unavailableRadiuses)
                && minPhase == constraints.minPhase
                && minStability == constraints.minStability;
        }

        public void SetMinRadius (int minRadius)
        {
            unavailableRadiuses.Clear();
            for (int i = 1; i < minRadius; i++)
                unavailableRadiuses.Add(i);
        }

        public void SetMaxRadius(int maxAvailableRadius, int totalMaxRadius)
        {
            unavailableRadiuses.Clear();
            for (int i = maxAvailableRadius + 1; i <= totalMaxRadius; i++)
                unavailableRadiuses.Add(i);
        }
    }

    class EventCard : IEquatable<EventCard>
    {
        private EventRelations _relations = new EventRelations();
        internal EventRelations relations
        {
            get { return _relations; }
            set {
                if (value == null)
                {
                    _relations = new EventRelations();
                } else
                {
                    _relations = value;
                }
            }
        }

        internal BranchPointsSet branchPoints = new BranchPointsSet(null, null);

        internal int stabilityBonus = 0;
        internal int miningBonus = 0;
        internal bool provideArtifact = false;
        internal bool isKey = false;
        internal bool isPairedReasons = false;

        internal EventConstraints constraints = new EventConstraints();

        internal float weight = 0;
        internal float usability = 0;
        internal string comment = "";
        internal string name = "";

        public EventCard() : base() { }

        public EventCard (EventCard card)
        {
            relations = new EventRelations(card.relations);
            branchPoints = new BranchPointsSet(card.branchPoints.success, card.branchPoints.failed);
            constraints.MakeEqual(card.constraints);

            stabilityBonus = card.stabilityBonus;
            miningBonus = card.miningBonus;
            provideArtifact = card.provideArtifact;
            isKey = card.isKey;
            isPairedReasons = card.isPairedReasons;

            weight = card.weight;
            usability = card.usability;
            comment = card.comment;
            name = card.name;
        }

        public bool Equals(EventCard other)
        {
            return relations.SequenceEqual(other.relations)
                && constraints.Equals(other.constraints)
                && stabilityBonus == other.stabilityBonus
                && miningBonus == other.miningBonus
                && provideArtifact == other.provideArtifact
                && isKey == other.isKey
                && isPairedReasons == other.isPairedReasons
                && branchPoints == other.branchPoints;
        }

        public bool hasBackReason()
        {
            return relations.Where(r => r.direction == RelationDirection.back && r.type == RelationType.reason).Count() > 0;
        }

        public int backRelationsCount()
        {
            return relations.Where(r => r.direction == RelationDirection.back).Count();
        }

        public int frontRelationsCount()
        {
            return relations.Where(r => r.direction == RelationDirection.front).Count();
        }
    }
}
