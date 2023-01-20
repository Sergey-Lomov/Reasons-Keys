using System;
using System.Collections.Generic;
using System.Linq;

using ModelAnalyzer.DataModels;

namespace ModelAnalyzer.Services.FieldAnalyzer
{
    using NodesTemplates = Dictionary<NodeTopologyType, NodeRelationsTemplate>;

    class NodesAnalyzer
    {
        private readonly NodesTemplates nodesTemplates = new NodesTemplates()
        {
            { NodeTopologyType.Center, new NodeRelationsTemplate("NNNNNN") },
            { NodeTopologyType.FirstInnerCorner, new NodeRelationsTemplate("PFNNNF") },
            { NodeTopologyType.NotFirstInnerCorner, new NodeRelationsTemplate("PBNNNF") },
            { NodeTopologyType.FirstOuterCorner, new NodeRelationsTemplate("PFOOOF") },
            { NodeTopologyType.NotFirstOuterCorner, new NodeRelationsTemplate("PBOOOF") },
            { NodeTopologyType.InnerSide, new NodeRelationsTemplate("PPBNNF") },
            { NodeTopologyType.OuterSide, new NodeRelationsTemplate("PPBOOF") },
            { NodeTopologyType.LastAtFirstRadius, new NodeRelationsTemplate("BNNNBP") },
            { NodeTopologyType.LastAtMiddleRadiuses, new NodeRelationsTemplate("PBNNBP") },
            { NodeTopologyType.LastAtLastRadius, new NodeRelationsTemplate("PBOOBP") }
        };

        internal int NodesOfType(NodeTopologyType type, int minRadius, int maxRadius)
        {
            switch (type) {
                case NodeTopologyType.Center:
                    return minRadius == 0 ? 1 : 0;
                case NodeTopologyType.FirstInnerCorner:
                    return maxRadius - Math.Max(minRadius, 1);
                case NodeTopologyType.NotFirstInnerCorner:
                    var decrement = minRadius <= 1 ? 1 : 0;
                    return 5 * (maxRadius - Math.Max(minRadius, 1)) - decrement;
                case NodeTopologyType.FirstOuterCorner:
                    return maxRadius > 0 ? 1 : 0;
                case NodeTopologyType.NotFirstOuterCorner:
                    return maxRadius > 0 ? 5 : 0;
                case NodeTopologyType.InnerSide:
                    int sum = 0;
                    for (int r = minRadius; r < maxRadius; r++) {
                        var radiusNodes = 6 * Math.Max(0, r - 1) - 1;
                        sum += Math.Max(0, radiusNodes);
                    }
                    return sum;
                case NodeTopologyType.OuterSide:
                    return maxRadius > 0 ? 6 * (maxRadius - 1) - 1 : 0;
                case NodeTopologyType.LastAtFirstRadius:
                    return minRadius <= 1 ? 1 : 0;
                case NodeTopologyType.LastAtMiddleRadiuses:
                    return maxRadius - Math.Max(minRadius - 1, 1) - 1;
                case NodeTopologyType.LastAtLastRadius:
                    return 1;
            }

            return 0;
        }

        internal int TemplatesCombinations(NodeTopologyType nodeType, EventRelationsTemplate eventTemplate) {
            var nodeTemplate = nodesTemplates[nodeType];
            return eventTemplate.variants.Where(v => IsCompatible(nodeTemplate, v)).Count();
        }

        private bool IsCompatible(NodeRelationsTemplate nodeTemplate, EventRelationsVariant variant)
        {
            if (nodeTemplate.relations.Count != variant.directions.Count)
            {
                return false;
            }

            for (int i = 0; i < nodeTemplate.relations.Count; i++)
            {
                var nodeRelation = nodeTemplate.relations[i];
                var direction = variant.directions[i];
                if (!IsCompatible(nodeRelation, direction))
                {
                    return false;
                }
            }

            return true;
        }

        private bool IsCompatible(NodeRelationType nodeRelation, RelationDirection? direction)
        {
            switch (direction)
            {
                case RelationDirection.none:
                    return true;
                case RelationDirection.back:
                    return nodeRelation == NodeRelationType.backPrev 
                        || nodeRelation == NodeRelationType.backSame;
                case RelationDirection.front:
                    return nodeRelation == NodeRelationType.frontNext
                        || nodeRelation == NodeRelationType.frontSame
                        || nodeRelation == NodeRelationType.frontOut;
            }

            return false;
        }
    }
}
