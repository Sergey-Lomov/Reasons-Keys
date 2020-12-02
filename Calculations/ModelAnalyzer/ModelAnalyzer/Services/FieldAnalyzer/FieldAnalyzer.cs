using System.Collections.Generic;
using ModelAnalyzer.DataModels;

namespace ModelAnalyzer.Services.FieldAnalyzer
{
    class FieldAnalyzer
    {
        internal Dictionary<int, Field> phasesFields = new Dictionary<int, Field>();
        private List<NodeTopologyType> availableNodeTypes = new List<NodeTopologyType>
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
        private Dictionary<NodeTopologyType, int> roundNodesOfType = new Dictionary<NodeTopologyType, int>();

        internal FieldAnalyzer(int phasesCount)
        {
            var factory = new FieldFabric();
            for (int phase = 0; phase < phasesCount; phase++)
                phasesFields[phase] = factory.field(phasesCount - 1, phase);
        }

        internal Dictionary<int, HashSet<FieldRoute>> phasesRoutes ()
        {
            var phasesRoutes = new Dictionary<int, HashSet<FieldRoute>>();

            foreach (var phaseField in phasesFields)
                phasesRoutes[phaseField.Key] = phaseField.Value.AllRoutes();

            return phasesRoutes;
        }

        internal void templateUsabilityPrecalculations(List<int> phasesDurations, int fieldRadius)
        {
            var nodesAnalyzer = new NodesAnalyzer();
            foreach (var type in availableNodeTypes)
            {
                int roundNodesAmount = 0;
                for (int i = 0; i < phasesDurations.Count; i++)
                {
                    var nodesAmount = nodesAnalyzer.nodesOfType(type, i, fieldRadius);
                    roundNodesAmount += nodesAmount * phasesDurations[i];
                }
                roundNodesOfType[type] = roundNodesAmount;
            }
        }

        internal float templateUsability(EventRelationsTemplate template, float totalRoundNodes)
        {
            var nodesAnalyzer = new NodesAnalyzer();
            int totalVariants = 0;
            foreach (var nodeType in availableNodeTypes)
            {
                var typeCombinations = nodesAnalyzer.templatesCombinations(nodeType, template);
                totalVariants += typeCombinations * roundNodesOfType[nodeType];
            }
            return totalVariants / totalRoundNodes;
        }
    }
}
