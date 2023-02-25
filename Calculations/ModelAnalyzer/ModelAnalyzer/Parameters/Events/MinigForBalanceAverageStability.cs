using ModelAnalyzer.Services;
using ModelAnalyzer.Parameters.Mining;
using ModelAnalyzer.Parameters.Activities;
using System;

namespace ModelAnalyzer.Parameters.Events
{
    class MinigForBalanceAverageStability : FloatSingleParameter
    {
        public MinigForBalanceAverageStability()
        {
            type = ParameterType.Indicator;
            title = "Кол-во актов добычи для уравновешивания среднего воздействия связей";
            details = "Этот параметр отражает кол-во актов добычи (средней добычи), необходимых для получения ТЗ, уравновешивающего воздействиями среднее воздействие связей карт.";
            fractionalDigits = 2;
            tags.Add(ParameterTag.events);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float eip = RequestParameter<EventImpactPrice>(calculator).GetValue();
            float am = RequestParameter<AverageMining>(calculator).GetValue();
            float arip = RequestParameter<AverageRelationsImpactPower>(calculator).GetValue();

            if (!calculationReport.IsSuccess)
                return calculationReport;

            value = unroundValue = arip * eip / am;

            return calculationReport;
        }
    }
}
