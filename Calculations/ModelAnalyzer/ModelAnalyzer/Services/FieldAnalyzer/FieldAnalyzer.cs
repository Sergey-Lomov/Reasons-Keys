using System;
using System.Collections.Generic;
using System.Linq;

using ModelAnalyzer.DataModels;

namespace ModelAnalyzer.Services.FieldAnalyzer
{
    class FieldAnalyzer
    {
        internal Dictionary<int, Field> phasesFields = new Dictionary<int, Field>();
        private readonly List<NodeTopologyType> availableNodeTypes = new List<NodeTopologyType>
        {
            NodeTopologyType.FirstInnerCorner,
            NodeTopologyType.FirstOuterCorner,
            NodeTopologyType.NotFirstInnerCorner,
            NodeTopologyType.NotFirstOuterCorner,
            NodeTopologyType.InnerSide,
            NodeTopologyType.OuterSide,
            NodeTopologyType.LastAtFirstRadius,
            NodeTopologyType.LastAtMiddleRadiuses,
            NodeTopologyType.LastAtLastRadius,
        };
        private readonly Dictionary<NodeTopologyType, int> roundNodesOfType = new Dictionary<NodeTopologyType, int>();

        internal FieldAnalyzer(int phasesCount)
        {
            var factory = new FieldFabric();
            for (int phase = 0; phase < phasesCount; phase++)
                phasesFields[phase] = factory.Field(phasesCount - 1, phase);
        }

        internal Dictionary<int, HashSet<FieldRoute>> PhasesRoutes ()
        {
            var phasesRoutes = new Dictionary<int, HashSet<FieldRoute>>();

            foreach (var phaseField in phasesFields)
                phasesRoutes[phaseField.Key] = phaseField.Value.AllRoutes();

            return phasesRoutes;
        }

        internal void TemplateUsabilityPrecalculations(List<int> phasesDurations, int fieldRadius)
        {
            var nodesAnalyzer = new NodesAnalyzer();
            foreach (var type in availableNodeTypes)
            {
                int roundNodesAmount = 0;
                for (int i = 0; i < phasesDurations.Count; i++)
                {
                    var nodesAmount = nodesAnalyzer.NodesOfType(type, i, fieldRadius);
                    roundNodesAmount += nodesAmount * phasesDurations[i];
                }
                roundNodesOfType[type] = roundNodesAmount;
            }
        }

        internal float TemplateUsability(EventRelationsTemplate template, float totalRoundNodes)
        {
            var nodesAnalyzer = new NodesAnalyzer();
            int totalVariants = 0;
            foreach (var nodeType in availableNodeTypes)
            {
                var typeCombinations = nodesAnalyzer.TemplatesCombinations(nodeType, template);
                totalVariants += typeCombinations * roundNodesOfType[nodeType];
            }
            return totalVariants / totalRoundNodes;
        }

        internal Dictionary<FieldPoint, List<RelationType>> AffectedPoints(int fieldPhase, EventCard card, FieldPoint point)
        {
            var result = new Dictionary<FieldPoint, List<RelationType>>();
            var field = phasesFields[fieldPhase];
            var relations = card.Relations;

            EventRelation rotate(EventRelation r, int i) => new EventRelation(r.type, r.direction, (r.position + i) % Field.nearesNodesAmount);
            for (int i = 0; i < Field.nearesNodesAmount; i++)
            {
                var rotated = relations.Select(r => rotate(r, i)).ToList();
                if (!RelationsValid(rotated, point))
                    continue;

                foreach (var relation in rotated)
                {
                    var fieldDirection = FieldDirection.FromEventRelationPosition(relation.position);
                    var target = new FieldPoint(point, fieldDirection);
                    if (!field.points.Contains(point))
                        continue;

                    if (!result.ContainsKey(target))
                        result[target] = new List<RelationType>();

                    result[target].Add(relation.type);
                }
            }
            return result;
        }

        private bool RelationsValid(List<EventRelation> relations, FieldPoint point)
        {
            foreach (var relation in relations)
            {
                if (relation.direction == RelationDirection.none)
                    continue;

                var fieldDirection = FieldDirection.FromEventRelationPosition(relation.position);
                var target = new FieldPoint(point, fieldDirection);

                if (relation.direction == RelationDirection.front && target.timestamp <= point.timestamp)
                    return false;
                if (relation.direction == RelationDirection.back && target.timestamp >= point.timestamp)
                    return false;
            }

            return true;
        }
    }
}
