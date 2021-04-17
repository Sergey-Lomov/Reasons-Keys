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
        private static readonly string deckElement = "Deck";
        private static readonly string cardElement = "Card";

        private static readonly string idElement = "id";
        private static readonly string mbElement = "mining_bonus";
        private static readonly string paElement = "provides_artifact";
        private static readonly string isKeyElement = "is_key";
        private static readonly string isPairedElement = "is_paired";
        private static readonly string urcElement = "unavailable_radiuses_constraint";
        private static readonly string weightElement = "weight";
        private static readonly string usabilityElement = "uisability";
        private static readonly string nameElement = "name";

        // BracnhPoint
        private static readonly string branchPointsElement = "BranchPoints";
        private static readonly string bpSuccessElement = "Success";
        private static readonly string bpFailedElement = "Failed";
        private static readonly string bpElement = "BranchPoint";

        private static readonly string branchElement = "branch";
        private static readonly string pointsElement = "points";

        // Relation
        private static readonly string relationsElement = "Relations";
        private static readonly string relationElement = "Relation";

        private static readonly string typeElement = "type";
        private static readonly string directionElement = "direction";
        private static readonly string positionElement = "position";

        private static readonly Dictionary<RelationType, string> typesStrings = new Dictionary<RelationType, string> {
            { RelationType.blocker, "blocker"},
            { RelationType.reason, "reason"},
            { RelationType.undefined, "undefined"},
         };

        private static readonly Dictionary<RelationDirection, string> directionsStrings = new Dictionary<RelationDirection, string> {
            { RelationDirection.front, "front"},
            { RelationDirection.back, "back"},
            { RelationDirection.none, "none"},
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
            writer.WriteElementString(paElement, card.provideArtifact.ToString());
            writer.WriteElementString(isKeyElement, card.isKey.ToString());
            writer.WriteElementString(isPairedElement, card.isPairedReasons.ToString());
            writer.WriteElementString(weightElement, card.weight.ToString("F0"));
            writer.WriteElementString(usabilityElement, card.usability.ToString("F1"));
            writer.WriteElementString(nameElement, card.name);

            WriteRadiusesConstraint(card.constraints.unavailableRadiuses, writer);
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

        private static void WriteRadiusesConstraint(List<int> unavailable, XmlWriter writer)
        {
            int mask = 0;
            foreach (var radius in unavailable)
                mask += 1 << radius;
            writer.WriteElementString(urcElement, mask.ToString());
        }
    }
}
