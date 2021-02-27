using System;
using System.Collections.Generic;
using System.Linq;

using ModelAnalyzer.DataModels;
using ModelAnalyzer.Parameters.Topology;
using ModelAnalyzer.Parameters.Activities;
using ModelAnalyzer.Parameters.Events.Weight;
using ModelAnalyzer.Parameters.Timing;
using ModelAnalyzer.Parameters.Items;
using ModelAnalyzer.Parameters.Events;

namespace ModelAnalyzer.Services
{
    class EventCardsAnalizer
    {
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

            var pd = calculator.UpdatedParameter<PhasesDuration>().GetValue();
            var fr = (int)calculator.UpdatedParameter<FieldRadius>().GetValue();
            var rna = (int)calculator.UpdatedParameter<RoundNodesAmount>().GetValue();

            var fieldAnalyzer = new FieldAnalyzer.FieldAnalyzer(phasesCount: pd.Count);
            var intPd = pd.Select(v => (int)v).ToList();
            fieldAnalyzer.templateUsabilityPrecalculations(intPd, fr);

            var template = EventRelationsTemplate.templateFor(relations, FieldAnalyzer.Field.nearesNodesAmount);
            return fieldAnalyzer.templateUsability(template, rna);
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
            var pr = 0; //TODO: Update formula regarding to new pairing reasons concept

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

        static internal List<EventCard> FirstFullCartage(EventCard card, List<EventCard> deck, int maxpa)
        {
            var cartage = new List<EventCard>();
            cartage.Add(card);
            var usedBranches = card.branchPoints.UsedBranches();

            if (usedBranches.Count() == 0)
                return cartage;

            bool isAvailable(EventCard c) => c.branchPoints.UsedBranches().Intersect(usedBranches).Count() == 0; 
            var templatedCards = deck.Where(c => c.branchPoints.Template() == card.branchPoints.Template()).ToList();
            var availableCards = templatedCards.Where(c => isAvailable(c)).ToList();

            while (usedBranches.Count() < maxpa && availableCards.Count() > 0)
            {
                var incomeCard = availableCards.First();
                cartage.Add(incomeCard);
                usedBranches.UnionWith(incomeCard.branchPoints.UsedBranches());
                availableCards = templatedCards.Where(c => isAvailable(c)).ToList();
            }

            return usedBranches.Count() == maxpa ? cartage : null;
        }
    }
}
