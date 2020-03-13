using System;
using System.Collections.Generic;
using System.Linq;

using ModelAnalyzer.DataModels;
using ModelAnalyzer.Parameters.Topology;
using ModelAnalyzer.Parameters.Activities;
using ModelAnalyzer.Parameters.Events.Weight;
using ModelAnalyzer.Parameters.Items;
using ModelAnalyzer.Parameters.Events;

namespace ModelAnalyzer.Services
{
    class EventCardsAnalizer
    {
        static private readonly string invalidIn = "Значение параметра {0} не верифицированно";

        static internal float WeightForCard(EventCard card, Calculator calculator)
        {
            float eap = calculator.UpdatedParameter<EstimatedArtifactsProfit>().GetValue();
            float eip = calculator.UpdatedParameter<EventImpactPrice>().GetValue();
            float mbw = calculator.UpdatedParameter<MiningBonusWeight>().GetValue();

            float weight = 0;
            weight += RelationsWeight(card.relations, calculator);
            weight += card.stabilityBonus * eip;
            weight += card.provideArtifact ? eap : 0;
            weight += card.miningBonus * mbw;

            return weight;
        }

        static internal float RelationsWeight (List<EventRelation> relations, Calculator calculator)
        {
            float frwc = calculator.UpdatedParameter<FrontRelationsWeightCoef>().GetValue();
            float brwc = calculator.UpdatedParameter<BackRelationsWeightCoef>().GetValue();
            float aсs = calculator.UpdatedParameter<AverageChainStability>().GetValue();
            float eip = calculator.UpdatedParameter<EventImpactPrice>().GetValue();
            float eun = calculator.UpdatedParameter<EventUsabilityNormalisation>().GetValue();

            Func<EventRelation, bool> frontPredicate = r => r.direction == RelationDirection.front;
            bool hasFront = relations.Where(frontPredicate).Count() > 0;

            float rtwc = hasFront ? frwc : brwc;
            float usability = RelationsUsability(relations, calculator);
            float noramalisedUsability = 1 + (usability - 1) * eun;

            return aсs * eip * noramalisedUsability * rtwc;
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
            // For now cards have no contraints 
           /* var aap = calculator.UpdatedParameter<ArtifactsAvaliabilityPhase>();
            if (!aap.VerifyValue())
            {
                var message = string.Format(invalidIn, aap.title);
                var e = new MACalculationException(message);
                throw e;
            }

            if (card.provideArtifact)
                card.minPhaseConstraint = (int)aap.GetValue();*/
        }

        static internal double PositiveRealisationChance(EventCard card, double aprc, double afra, double afba, double bric)
        {
            bool isBack(EventRelation rel) => rel.direction == RelationDirection.back;
            var relations = card.relations.Where(rel => isBack(rel));
            var b = (double)relations.Where(rel => rel.type == RelationType.blocker).Count();
            var r = (double)relations.Where(rel => rel.type == RelationType.reason).Count();
            var pr = (double)relations.Where(rel => rel.type == RelationType.paired_reason).Count();

            var aab = b * (1 - bric) + afba;
            var aar = r * (1 - bric) + afra;
            var aapr = pr * (1 - bric);

            var anrc = 1 - aprc;
            var bComponent = aab <= 1 ? 1 - aprc * aab : Math.Pow(anrc, aab);
            var rComponent = aar <= 1 ? 1 - anrc * aar : 1 - Math.Pow(anrc, aar);
            var prComponent = aapr <= 1 ? 1 - anrc * aapr : Math.Pow(aprc, aapr);

            return bComponent * rComponent * prComponent;
        }

        static internal List<float> PointsByAppend(List<float> points, EventCard card)
        {
            return PointsByAppend(points, card, card.branchPoints);
        }

        static internal List<float> PointsByAppend(List<float> points, EventCard card, BranchPointsSet bpSet, bool sign = true)
        {
            var newPoints = new List<float>(points);
            foreach (var bp in bpSet.success)
            {
                float point = sign ? bp.point : Math.Abs(bp.point);
                newPoints[bp.branch] += point * card.positiveRealisationChance;
            }
            foreach (var bp in bpSet.failed)
            {
                float point = sign ? bp.point : Math.Abs(bp.point);
                newPoints[bp.branch] += point * (1 - card.positiveRealisationChance);
            }
            return newPoints;
        }
    }
}
