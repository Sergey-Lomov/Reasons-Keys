using System;
using System.Collections.Generic;
using System.Linq;

using ModelAnalyzer.DataModels;
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

        abstract internal List<EventCard> Deck(Calculator calculator);

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            var pa = (int)calculator.UpdatedParameter<PhasesAmount>().GetValue();
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

            Func<EventRelation, bool> validRel = 
                r => handleDirections.Contains(r.direction) && handleTypes.Contains(r.type);
            Func<EventCard, bool> hasValidRel = c => c.relations.Where(r => validRel(r)).Count() > 0;

            var handlingDeck = deck.Where(c => hasValidRel(c));
            foreach (var card in handlingDeck)
            {
                foreach (var point in points)
                {
                    var impacts = fieldAnalyzer.affectedPoints(0, card, point);
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

        public override float deviationForValue(float value)
        {
            if (useRelative)
                return Math.Min(1, value / 2);
            else
                return (value - minValue) / (averageValue - minValue) / 2;
        }
    }
}
