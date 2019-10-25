using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

using ModelAnalyzer.DataModels;

namespace ModelAnalyzer.Services
{
    class DeckXMLGenerator
    {
        // Elements
        private static string deckElement = "Deck";
        private static string cardElement = "Card";

        private static string relationsElement = "Relations";
        private static string relationElement = "Relation";

        private static string branchPointsElement = "BranchPoints";
        private static string bpSuccessElement = "Success";
        private static string bpFailedElement = "Failed";
        private static string bpElement = "BranchPoint";

        // Card
        private static string idAttribute = "id";
        private static string mbAttribute = "mining_bonus";
        private static string siAttribute = "stability_increment";
        private static string paAttribute = "provides_artifact";
        private static string isKeyAttribute = "is_key";
        private static string msKeyAttribute = "min_stability_constraint";
        private static string weightAttribute = "weight";
        private static string usabilityAttribute = "uisability";

        // BracnhPoint
        private static string branchAttribute = "branch";
        private static string pointsAttribute = "points";

        // Relation
        private static string typeAttribute = "type";
        private static string directionAttribute = "direction";
        private static string positionAttribute = "position";

        private static Dictionary<RelationType, string> typesStrings = new Dictionary<RelationType, string> {
            { RelationType.blocker, "blocker"},
            { RelationType.reason, "reason"},
            { RelationType.paired_reason, "paired_reason"}
         };

        private static Dictionary<RelationDirection, string> directionsStrings = new Dictionary<RelationDirection, string> {
            { RelationDirection.front, "front"},
            { RelationDirection.back, "back"}
         };

        internal static void GenerateXML (List<EventCard> deck, string filePath)
        {
            XmlWriter writer = XmlWriter.Create(filePath, new XmlWriterSettings
            {
                ConformanceLevel = ConformanceLevel.Document,
                Encoding = Encoding.Unicode
            });

            writer.WriteStartDocument();
            writer.WriteStartElement(deckElement);

            foreach (var card in deck)
            {
                var index = deck.FindIndex(c => ReferenceEquals(c, card)) + 1;
                WriteCard(card, index, writer);
            }

            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Close();
        }

        private static void WriteCard (EventCard card, int index, XmlWriter writer)
        {
            writer.WriteStartElement(cardElement);

            // Attributes
            writer.WriteElementString(idAttribute, index.ToString());
            writer.WriteElementString(mbAttribute, card.miningBonus.ToString());
            writer.WriteElementString(siAttribute, card.stabilityIncrement.ToString());
            writer.WriteElementString(paAttribute, card.provideArtifact.ToString());
            writer.WriteElementString(isKeyAttribute, card.isKey.ToString());
            writer.WriteElementString(msKeyAttribute, card.minStabilityConstraint.ToString());
            writer.WriteElementString(weightAttribute, card.weight.ToString("F0"));
            writer.WriteElementString(usabilityAttribute, card.usability.ToString("F1"));

            WriteBranchPoints(card.branchPoints, writer);
            WriteRelations(card.relations, writer);

            writer.WriteEndElement();
        }

        private static void WriteBranchPoints (BranchPointsSet bps, XmlWriter writer)
        {
            writer.WriteStartElement(branchPointsElement);

            writer.WriteStartElement(bpSuccessElement);
            foreach (var bp in bps.success)
                WriteBranchPoint(bp, writer);
            writer.WriteEndElement();

            writer.WriteStartElement(bpFailedElement);
            foreach (var bp in bps.failed)
                WriteBranchPoint(bp, writer);
            writer.WriteEndElement();

            writer.WriteEndElement();
        }

        private static void WriteBranchPoint (BranchPoint bp, XmlWriter writer)
        {
            writer.WriteStartElement(bpElement);
            writer.WriteElementString(branchAttribute, bp.branch.ToString());
            writer.WriteElementString(pointsAttribute, bp.point.ToString());
            writer.WriteEndElement();
        }

        private static void WriteRelations (List<EventRelation> relations, XmlWriter writer)
        {
            writer.WriteStartElement(relationsElement);

            foreach (var relation in relations)
                WriteRelation(relation, writer);

            writer.WriteEndElement();
        }

        private static void WriteRelation(EventRelation relation, XmlWriter writer)
        {
            writer.WriteStartElement(relationElement);
            writer.WriteElementString(typeAttribute, typesStrings[relation.type]);
            writer.WriteElementString(directionAttribute, directionsStrings[relation.direction]);
            writer.WriteElementString(positionAttribute, relation.position.ToString());
            writer.WriteEndElement();
        }
    }
}
