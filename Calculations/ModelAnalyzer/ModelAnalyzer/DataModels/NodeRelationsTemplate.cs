using System.Collections.Generic;

namespace ModelAnalyzer.DataModels
{
    enum NodeRelationType
    {
        backPrev, backSame, frontSame, frontNext, frontOut
    }

    class NodeRelationsTemplate
    {
        private readonly Dictionary<char, NodeRelationType> marksRelations = new Dictionary<char, NodeRelationType> {
            { 'O', NodeRelationType.frontOut},
            { 'N', NodeRelationType.frontNext},
            { 'F', NodeRelationType.frontSame},
            { 'B', NodeRelationType.backSame},
            { 'P', NodeRelationType.backSame}
        };

        public List<NodeRelationType> relations = new List<NodeRelationType>();

        public NodeRelationsTemplate(string code)
        {
            for (int i = 0; i < code.Length && i < 6; i++)
            {
                var mark = code[i];
                var relation = marksRelations[mark];
                relations.Add(relation);
            }
        }
    }
}
