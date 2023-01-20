using System;
using System.Collections.Generic;
using System.Linq;

namespace ModelAnalyzer.DataModels
{
    class EventRelationsVariant: IEquatable<EventRelationsVariant>
    {
        public List<RelationDirection> directions = new List<RelationDirection>();

        internal static Dictionary<RelationDirection, char> directionsMarks = new Dictionary<RelationDirection, char> {
            {RelationDirection.none, 'N'},
            {RelationDirection.front, 'F' },
            {RelationDirection.back, 'B' }
        };

        public string Code
        {
            get
            {
                var marks = directions.Select(d => directionsMarks[d]);
                return string.Join("", marks);
            }
        }

        public EventRelationsVariant(string code)
        {
            for (int i = 0; i < code.Length; i++)
            {
                var mark = code[i];
                var direction = directionsMarks.FirstOrDefault(x => x.Value == mark).Key;
                directions.Add(direction);
            }
        }

        public bool Equals(EventRelationsVariant other)
        {
            return directions.SequenceEqual(other.directions);
        }

        internal List<EventRelation> InstantiateByReasons()
        {
            var relations = new List<EventRelation>();
            for (int i = 0; i < directions.Count; i++)
            {
                if (directions[i] == RelationDirection.none)
                    continue;
                
                var relation = new EventRelation(RelationType.reason, directions[i], i);
                relations.Add(relation);
            }

            return relations;
        }
    }

    class EventRelationsTemplate
    {
        public List<EventRelationsVariant> variants = new List<EventRelationsVariant>();
        private readonly string doubleCode;

        public EventRelationsTemplate(string code)
        {
            for (int i = 0; i < code.Length; i++)
            {
                var left = code.Substring(0, i);
                var right = code.Substring(i);
                var variantCode = right + left;
                variants.Add(new EventRelationsVariant(variantCode));
            }

            doubleCode = code + code;
        }

        public static EventRelationsTemplate TemplateFor(List<EventRelation> relations, int totalRelations)
        {
            var emptyMark = EventRelationsVariant.directionsMarks[RelationDirection.none];
            var components = Enumerable.Repeat(emptyMark, totalRelations).ToArray();
            foreach (var relation in relations)
            {
                var mark = EventRelationsVariant.directionsMarks[relation.direction];
                components[relation.position] = mark;
            }
            var code = String.Join("", components);
            return new EventRelationsTemplate(code);
        }

        public static List<EventRelationsTemplate> AllTemplates(int slotsAmount)
        {
            var templates = new List<EventRelationsTemplate>();

            var marks = EventRelationsVariant.directionsMarks.Values.Select(x => x.ToString());
            var variantsCodes = new List<string>(marks);
            for (int i = 0; i < slotsAmount - 1; i++)
                variantsCodes = variantsCodes.SelectMany(x => marks, (x, y) => x + y).ToList();

            foreach (var code in variantsCodes)
            {
                var alreadyExist = templates.Where(t => t.ContainsVariant(code)).Count() > 0;
                if (!alreadyExist)
                {
                    templates.Add(new EventRelationsTemplate(code));
                }
            }

            return templates;
        }

        private bool ContainsVariant(string code)
        {
            return doubleCode.Contains(code);
        }

        internal bool ContainsBack()
        {
            return variants.First().directions.Contains(RelationDirection.back);
        }

        internal int DirectionsAmount()
        {
            return variants.First().directions.Where(d => d != RelationDirection.none).Count();
        }

        internal int BackAmount()
        {
            return variants.First().directions.Where( d => d == RelationDirection.back).Count();
        }

        internal bool ContainsFront()
        {
            return variants.First().directions.Contains(RelationDirection.front);
        }

        internal List<EventRelation> InstantiateByReasons()
        {
            if (variants.Count > 0)
            {
                return variants[0].InstantiateByReasons();
            }

            return new EventRelationsVariant("NNNNNN").InstantiateByReasons();
        }
    }

    class RelationsTemplateUsageInfo
    {
        public int cardsCount;
        public float usability;
    }
}
