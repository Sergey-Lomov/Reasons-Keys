using System;

using ModelAnalyzer.Services;
using ModelAnalyzer.Parameters.Activities;

namespace ModelAnalyzer.Parameters.PlayerInitial
{
    class LogisticInitialEventTotalPower : FloatSingleParameter
    {
        public LogisticInitialEventTotalPower()
        {
            type = ParameterType.Out;
            title = "ЛИС: сила связей вперед";
            details = "Максимальная разрешенная суммарная сила всех связей вперед на логистической изначальной карте";
            fractionalDigits = 0;
            tags.Add(ParameterTag.playerInitial);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float eop = RequestParmeter<EventCreationPrice>(calculator).GetValue();
            float eip = RequestParmeter<EventImpactPrice>(calculator).GetValue();
            float liepc = RequestParmeter<LogisticInitialEventPowerCoefficient>(calculator).GetValue();

            if (!calculationReport.IsSuccess)
                return calculationReport;

            unroundValue = eop / eip * liepc;
            value = (int)Math.Round(unroundValue, MidpointRounding.AwayFromZero);

            return calculationReport;
        }
    }
}
