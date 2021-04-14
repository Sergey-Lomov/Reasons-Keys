using ModelAnalyzer.Services;
using System;

namespace ModelAnalyzer.Parameters.Items.Artifacts.CollectorModule
{
    class CM_UsageAmount : FloatSingleParameter
    {
        public CM_UsageAmount()
        {
            type = ParameterType.Out;
            title = "МК: кол-во испольозваний";
            details = "";
            fractionalDigits = 0;
            tags.Add(ParameterTag.items);
            tags.Add(ParameterTag.artifacts);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            var module = RequestModule<CM_CalculationModule>(calculator);
            value = unroundValue = module.usageAmount;

            return calculationReport;
        }
    }
}
