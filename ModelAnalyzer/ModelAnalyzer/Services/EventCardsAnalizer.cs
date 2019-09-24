using System;
using System.Collections.Generic;
using System.Linq;

using ModelAnalyzer.DataModels;
using ModelAnalyzer.Parameters.Activities;
using ModelAnalyzer.Parameters.Mining;
using ModelAnalyzer.Parameters.Events.Weight;

namespace ModelAnalyzer.Services
{
    class EventCardsAnalizer
    {
        static internal float WeightForCard(EventCard card, Calculator calculator)
        {
            float aw = calculator.UpdatedSingleValue(typeof(ArtifactsWeight));
            float am = calculator.UpdatedSingleValue(typeof(AverageMining));
            float eip = calculator.UpdatedSingleValue(typeof(EventImpactPrice));
            float mbw = calculator.UpdatedSingleValue(typeof(MiningBonusWeight));

            float weight = 0;
            weight += RelationsWeight(card.relations, calculator);
            weight += card.stabilityIncrement * eip;
            weight += card.provideArtifact ? aw * am : 0;
            weight += card.miningBonus * mbw;

            return weight;
        }

        static internal float RelationsWeight (List<EventRelation> relations, Calculator calculator)
        {
            float brw = calculator.UpdatedSingleValue(typeof(BaseRelationsWeight));
            float arw = calculator.UpdatedSingleValue(typeof(AdditionalReasonsWeight));
            float frw = calculator.UpdatedSingleValue(typeof(FrontReasonsWeight));
            float fbw = calculator.UpdatedSingleValue(typeof(FrontBlockerWeight));

            float ars = calculator.UpdatedSingleValue(typeof(AverageRelationStability));
            float eip = calculator.UpdatedSingleValue(typeof(EventImpactPrice));
            float eun = calculator.UpdatedSingleValue(typeof(EventUsabilityNormalisation));


            Func<EventRelation, bool> basePredicate = r => r.direction == RelationDirection.back && r.type != RelationType.reason;
            Func<EventRelation, bool> backReasonPredicate = r => r.direction == RelationDirection.back && r.type == RelationType.reason;
            Func<EventRelation, bool> frontReasonPredicate = r => r.direction == RelationDirection.front && r.type == RelationType.reason;
            Func<EventRelation, bool> frontBlockPredicate = r => r.direction == RelationDirection.front && r.type == RelationType.blocker;

            int baseAmount = relations.Where(basePredicate).Count();
            int backReasonsAmount = relations.Where(backReasonPredicate).Count();
            int frontReasonAmount = relations.Where(frontReasonPredicate).Count();
            int frontBlockAmount = relations.Where(frontBlockPredicate).Count();

            float baseWeight = baseAmount * brw + frontReasonAmount * frw + frontBlockAmount * fbw;
            if (backReasonsAmount > 0)
            {
                baseWeight += brw;
                baseWeight += (backReasonsAmount - 1) * arw;
            }

            float usability = RelationsUsability(relations, calculator);
            float noramalisedUsability = 1 + (usability - 1) * eun;

            return baseWeight * noramalisedUsability * ars * eip;
        }

        static public float RelationsUsability(List<EventRelation> relations, Calculator calculator)
        {
            if (relations.Count == 0)
                return 0;

            float[] availableBackAllocation = calculator.UpdatedArrayValue(typeof(NodesAvailableBackRelations));

            int backAmount = relations.Where(r => r.direction == RelationDirection.back).Count();
            int frontAmount = relations.Where(r => r.direction == RelationDirection.front).Count();

            int backRange = 0;
            int frontRange = 0;
            var back = relations.Where(r => r.direction == RelationDirection.back).OrderBy(r => r.position);
            var front = relations.Where(r => r.direction == RelationDirection.front).OrderBy(r => r.position);

            if (back.Count() > 0)
                backRange = back.Last().position - back.First().position + 1;

            if (front.Count() > 0)
                frontRange = front.Last().position - front.First().position + 1;

            int combintaions = 0;
            for (int availableBack = 0; availableBack < availableBackAllocation.Count(); availableBack++)
            {
                int availableFront = EventRelation.MaxRelationPosition - availableBack;
                if (backAmount > availableBack || frontAmount > availableFront)
                    continue;

                List<int> constraints = new List<int>();

                if (backRange > 0)
                    constraints.Add(availableBack - backRange + 1);
                if (frontRange > 0)
                    constraints.Add(availableFront - frontRange + 1);
                if (backRange > 0 && frontRange > 0)
                {
                    var clocwiseConstraint = front.First().position - back.Last().position;
                    var unclocwiseConstraint = (EventRelation.MaxRelationPosition - front.Last().position) + back.First().position;
                    constraints.Add(clocwiseConstraint);
                    constraints.Add(unclocwiseConstraint);
                }

                var rotations = constraints.Min();
                combintaions += (int)availableBackAllocation[availableBack] * rotations;
            }

            var totalNodesAmount = availableBackAllocation.Sum();
            return combintaions / totalNodesAmount;
        }
    }
}
