using System;
using System.Collections.Generic;
using System.Linq;

using ModelAnalyzer.DataModels;
using ModelAnalyzer.Parameters.Topology;
using ModelAnalyzer.Parameters.Activities;
using ModelAnalyzer.Parameters.Mining;
using ModelAnalyzer.Parameters.Events.Weight;
using ModelAnalyzer.Parameters.Items;

namespace ModelAnalyzer.Services
{
    class EventCardsAnalizer
    {
        static private readonly string invalidIn = "Значение параметра {0} не верифицированно";

        static internal float WeightForCard(EventCard card, Calculator calculator)
        {
            float aw = calculator.UpdatedParameter<ArtifactsWeight>().GetValue();
            float am = calculator.UpdatedParameter<AverageMining>().GetValue();
            float eip = calculator.UpdatedParameter<EventImpactPrice>().GetValue();
            float mbw = calculator.UpdatedParameter<MiningBonusWeight>().GetValue();

            float weight = 0;
            weight += RelationsWeight(card.relations, calculator);
            weight += card.stabilityIncrement * eip;
            weight += card.provideArtifact ? aw * am : 0;
            weight += card.miningBonus * mbw;

            return weight;
        }

        static internal float RelationsWeight (List<EventRelation> relations, Calculator calculator)
        {
            float brw = calculator.UpdatedParameter<BaseRelationsWeight>().GetValue();
            float arw = calculator.UpdatedParameter<AdditionalReasonsWeight>().GetValue();
            float frw = calculator.UpdatedParameter<FrontReasonsWeight>().GetValue();
            float fbw = calculator.UpdatedParameter<FrontBlockerWeight>().GetValue();

            float ars = calculator.UpdatedParameter<AverageRelationStability>().GetValue();
            float eip = calculator.UpdatedParameter<EventImpactPrice>().GetValue();
            float eun = calculator.UpdatedParameter<EventUsabilityNormalisation>().GetValue();

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

            var availableBackAllocationParameter = calculator.UpdatedParameter<NodesAvailableBackRelations>();
            if (!availableBackAllocationParameter.VerifyValue())
            {
                var message = string.Format(invalidIn, availableBackAllocationParameter.title);
                var e = new MACalculationException(message);
                throw e;
            }

            List<float> availableBackAllocation = availableBackAllocationParameter.GetValue();
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

        static internal void UpdateCardConstraints (EventCard card, Calculator calculator)
        {
            var aap = calculator.UpdatedParameter<ArtifactsAvaliabilityPhase>();
            if (!aap.VerifyValue())
            {
                var message = string.Format(invalidIn, aap.title);
                var e = new MACalculationException(message);
                throw e;
            }

            if (card.provideArtifact)
                card.minPhaseConstraint = (int)aap.GetValue();
        }
    }
}
