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
            var aripc = calculator.UpdatedParameter<AverageRelationsImpactPerCount>().GetValue();
            float rip = calculator.UpdatedParameter<RelationImpactPower>().GetValue();
            float eip = calculator.UpdatedParameter<EventImpactPrice>().GetValue();
            float eun = calculator.UpdatedParameter<EventUsabilityNormalisation>().GetValue();

            Func<EventRelation, bool> frontPredicate = r => r.direction == RelationDirection.front;
            bool hasFront = relations.Where(frontPredicate).Count() > 0;

            float rtwc = hasFront ? frwc : brwc;
            float usability = RelationsUsability(relations, calculator);
            float noramalisedUsability = 1 + (usability - 1) * eun;

            int backCount = relations.Where(r => r.direction == RelationDirection.back).Count();

            if (backCount >= aripc.Count)
                throw new Exception("Constant AverageRelationImpactCoefficient have less items, then some card back relations count");

            return aripc[backCount] * rip * eip * noramalisedUsability * rtwc;
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

        static internal Dictionary<EventCard, float> CardsStabilities(List<EventCard> deck, List<float> aripc)
        {
            float cardStability(EventCard c) => c.stabilityBonus + aripc[c.backRelationsCount()];
            return deck.ToDictionary(c => c, c => cardStability(c));
        }

        static internal float BrancheDisbalance(List<EventCard> deck, 
            Dictionary<EventCard, float> stabilities,
            int maxpa,
            List<BranchPointsSet> bpSets = null)
        {
            if (bpSets == null)
            {
                bpSets = deck.Select(c => c.branchPoints).ToList();
            }

            List<float> playerPositiveStability = Enumerable.Repeat(0f, maxpa).ToList();
            List<float> playerNegativeStability = Enumerable.Repeat(0f, maxpa).ToList();
            List<float> playerForwardUsability = Enumerable.Repeat(0f, maxpa).ToList();
            for (int j = 0; j < deck.Count(); j++)
            {
                var card = deck[j];
                var stability = stabilities[card];
                var branchPoints = bpSets[j].failed.Concat(bpSets[j].success);
                foreach (var bp in branchPoints)
                {
                    var targetList = bp.point > 0 ? playerPositiveStability : playerNegativeStability;
                    targetList[bp.branch] += stability;
                }

                if (card.frontRelationsCount() == 0) continue;

                var winners = branchPoints.Where(bp => bp.point > 0).Select(bp => bp.branch).ToList();
                if (winners.Count > 0)
                {
                    foreach (var player in winners)
                        playerForwardUsability[player] += card.usability;
                } else
                {
                    var loosers = branchPoints.Where(bp => bp.point < 0).Select(bp => bp.branch).ToList();
                    foreach (var player in Enumerable.Range(0, maxpa))
                        if (!loosers.Contains(player))
                            playerForwardUsability[player] += card.usability;
                }
            }

            float relatedDevoition(List<float> list) => (list.Max() - list.Min()) / list.Average();
            var devoitions = new List<float>() {
                relatedDevoition(playerPositiveStability),
                relatedDevoition(playerNegativeStability),
                relatedDevoition(playerForwardUsability)
            };

            return devoitions.Max();
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
                newPoints[bp.branch] += point * 0.5f;
            }
            foreach (var bp in bpSet.failed)
            {
                float point = sign ? bp.point : Math.Abs(bp.point);
                newPoints[bp.branch] += point * 0.5f;
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
