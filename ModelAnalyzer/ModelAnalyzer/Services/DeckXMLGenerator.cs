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
        // Card
        private static string deckElement = "Deck";
        private static string cardElement = "Card";

        private static string idElement = "id";
        private static string mbElement = "mining_bonus";
        private static string siElement = "stability_increment";
        private static string paElement = "provides_artifact";
        private static string isKeyElement = "is_key";
        private static string mscElement = "min_stability_constraint";
        private static string weightElement = "weight";
        private static string usabilityElement = "uisability";

        // BracnhPoint
        private static string branchPointsElement = "BranchPoints";
        private static string bpSuccessElement = "Success";
        private static string bpFailedElement = "Failed";
        private static string bpElement = "BranchPoint";

        private static string branchElement = "branch";
        private static string pointsElement = "points";

        // Relation
        private static string relationsElement = "Relations";
        private static string relationElement = "Relation";

        private static string typeElement = "type";
        private static string directionElement = "direction";
        private static string positionElement = "position";

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

            // Common elements
            writer.WriteElementString(idElement, index.ToString());
            writer.WriteElementString(mbElement, card.miningBonus.ToString());
            writer.WriteElementString(siElement, card.stabilityIncrement.ToString());
            writer.WriteElementString(paElement, card.provideArtifact.ToString());
            writer.WriteElementString(isKeyElement, card.isKey.ToString());
            writer.WriteElementString(mscElement, card.minStabilityConstraint.ToString());
            writer.WriteElementString(weightElement, card.weight.ToString("F0"));
            writer.WriteElementString(usabilityElement, card.usability.ToString("F1"));

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
            writer.WriteElementString(branchElement, bp.branch.ToString());
            writer.WriteElementString(pointsElement, bp.point.ToString());
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
            writer.WriteElementString(typeElement, typesStrings[relation.type]);
            writer.WriteElementString(directionElement, directionsStrings[relation.direction]);
            writer.WriteElementString(positionElement, relation.position.ToString());
            writer.WriteEndElement();
        }
    }
}
