using System;
using ModelAnalyzer.Services;

namespace ModelAnalyzer.Parameters.Items.Artifacts.CollectorModule
{
    class CM_MinLimit : FloatSingleParameter
    {
        public CM_MinLimit()
        {
            type = ParameterType.Out;
            title = "МК: минимальный лимит прибавки";
            details = "Минимум, который МК гарантированно приносит игроку при добыче";
            fractionalDigits = 2;
            tags.Add(ParameterTag.items);
            tags.Add(ParameterTag.artifacts);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            var module = RequestModule<CM_CalculationModule>(calculator);
            unroundValue = module.minLimit;
            value = (float)Math.Round(unroundValue, MidpointRounding.AwayFromZero);

            return calculationReport;
        }
    }
}
