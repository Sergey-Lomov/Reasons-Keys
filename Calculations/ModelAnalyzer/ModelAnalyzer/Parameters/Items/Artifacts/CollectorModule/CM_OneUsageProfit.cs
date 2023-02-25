using ModelAnalyzer.Services;

namespace ModelAnalyzer.Parameters.Items.Artifacts.CollectorModule
{
    class CM_OneUsageProfit : FloatSingleParameter
    {
        public CM_OneUsageProfit()
        {
            type = ParameterType.Inner;
            title = "МК: выгодность одного использования";
            details = "";
            fractionalDigits = 2;
            tags.Add(ParameterTag.items);
            tags.Add(ParameterTag.artifacts);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            var module = RequestModule<CM_CalculationModule>(calculator);
            value = unroundValue = module.usageProfit;

            return calculationReport;
        }
    }
}
