using ModelAnalyzer.Services;
using ModelAnalyzer.Parameters.Events;

namespace ModelAnalyzer.Parameters.PlayerInitial
{
    class LogisticInitialEventTotalPower : FloatSingleParameter
    {
        public LogisticInitialEventTotalPower()
        {
            type = ParameterType.Out;
            title = "ЛИС: сила связей вперед";
            details = "Максимальная разрешенная суммарная сила всех связей на логистической изначальной карте";
            fractionalDigits = 0;
            tags.Add(ParameterTag.playerInitial);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float rip = RequestParameter<RelationImpactPower>(calculator).GetValue();
            float lieftp = RequestParameter<LogisticInitialEventForceTotalPower>(calculator).GetValue();

            if (!calculationReport.IsSuccess)
                return calculationReport;

            value = rip + lieftp;

            return calculationReport;
        }
    }
}
