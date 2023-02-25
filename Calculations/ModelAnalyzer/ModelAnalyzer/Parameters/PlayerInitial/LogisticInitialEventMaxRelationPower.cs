using System;
using System.Linq;

using ModelAnalyzer.Services;
using ModelAnalyzer.Parameters.Events;

namespace ModelAnalyzer.Parameters.PlayerInitial
{
    class LogisticInitialEventMaxRelationPower : FloatSingleParameter
    {
        public LogisticInitialEventMaxRelationPower()
        {
            type = ParameterType.Out;
            title = "ЛИС: максимальная сила связи";
            details = "Параметр, определяющий максимальное воздействие, которое можно направить из логисчтического изначального события на одно событие";
            fractionalDigits = 0;
            tags.Add(ParameterTag.playerInitial);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float rip = RequestParameter<RelationImpactPower>(calculator).GetValue();
            var mainDeck = RequestParameter<MainDeck>(calculator).deck;

            if (!calculationReport.IsSuccess)
                return calculationReport;

            var maxFrontRelations = mainDeck.Select(c => c.FrontRelationsCount()).Max();
            unroundValue = maxFrontRelations * rip;
            value = unroundValue = (int)Math.Round(unroundValue, MidpointRounding.AwayFromZero);

            return calculationReport;
        }
    }
}
