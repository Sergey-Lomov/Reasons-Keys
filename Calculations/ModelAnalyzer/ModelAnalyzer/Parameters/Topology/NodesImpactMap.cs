using System;
using System.Collections.Generic;
using System.Linq;

using ModelAnalyzer.DataModels;
using ModelAnalyzer.Parameters.Events;
using ModelAnalyzer.Parameters.PlayerInitial;
using ModelAnalyzer.Parameters.Timing;
using ModelAnalyzer.Services;
using ModelAnalyzer.Services.FieldAnalyzer;

namespace ModelAnalyzer.Parameters.Topology
{
    abstract class NodesImpactMap : FieldNodesFloatParameter
    {
        private float minValue;
        private float averageValue;

        protected List<RelationDirection> handleDirections = new List<RelationDirection>();
        protected List<RelationType> handleTypes = new List<RelationType>();
        protected bool useRelative = false;
        protected bool useInitialDeck = true;
        protected bool useContinuumDeck = true;

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            var pa = (int)RequestParameter<PhasesAmount>(calculator).GetValue();
            var deck = Deck(calculator);

            if (!calculationReport.IsSuccess)
                return calculationReport;

            field = new Dictionary<FieldPoint, float>();
            var fieldAnalyzer = new FieldAnalyzer(pa);
            var points = fieldAnalyzer.phasesFields[0].points;
            points.Remove(FieldPoint.center);
            foreach (var point in points)
            {
                field[point] = 0;
            }

            bool validRel(EventRelation r) => handleDirections.Contains(r.direction) && handleTypes.Contains(r.type);
            bool hasValidRel(EventCard c) => c.Relations.Where(r => validRel(r)).Count() > 0;

            var handlingDeck = deck.Where(c => hasValidRel(c));
            foreach (var card in handlingDeck)
            {
                foreach (var point in points)
                {
                    var impacts = fieldAnalyzer.AffectedPoints(0, card, point);
                    var validImpacts = impacts.Where(kvp => kvp.Value.Intersect(handleTypes).Count() > 0);
                    foreach (var impact in validImpacts)
                    {
                        var target = impact.Key;
                        if (points.Contains(target) && point.timestamp < target.timestamp)
                            field[impact.Key] += 1;
                    }
                }
            }

            if (useRelative)
            {
                foreach (var point in points)
                {
                    field[point] /= handlingDeck.Count();
                }
            }

            minValue = field.Values.Min();
            averageValue = field.Values.Average();

            return calculationReport;
        }

        public override float DeviationForValue(float value)
        {
            if (useRelative)
                return Math.Min(1, value / 2);
            else
                return (value - minValue) / (averageValue - minValue) / 2;
        }

        internal List<EventCard> Deck(Calculator calculator)
        {
            var continuumDeck = RequestParameter<MainDeck>(calculator).deck;
            var initialDeck = RequestParameter<StartDeck>(calculator).deck;

            var result = new List<EventCard>();
            if (!calculationReport.IsSuccess)
                return result;

            if (useContinuumDeck)
                result.AddRange(continuumDeck);
            if (useInitialDeck)
                result.AddRange(initialDeck);
            return result;
        }
    }
}
