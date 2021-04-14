using System;
using ModelAnalyzer.Services;

namespace ModelAnalyzer.Parameters.Items.Artifacts.CollectorModule
{
    class CM_MaxLimit : FloatSingleParameter
    {
        public CM_MaxLimit()
        {
            type = ParameterType.Out;
            title = "МК: максимальный лимит прибавки";
            details = "Максимумальное ТЗ, которое МК может принести за 1 добычу";
            fractionalDigits = 0;
            tags.Add(ParameterTag.items);
            tags.Add(ParameterTag.artifacts);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            var module = RequestModule<CM_CalculationModule>(calculator);
            unroundValue = module.maxLimit;
            value = (float)Math.Round(unroundValue, MidpointRounding.AwayFromZero);

            return calculationReport;
        }
    }
}
