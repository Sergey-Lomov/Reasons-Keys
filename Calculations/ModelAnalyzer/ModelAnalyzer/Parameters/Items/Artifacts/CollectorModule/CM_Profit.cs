using ModelAnalyzer.Services;

namespace ModelAnalyzer.Parameters.Items.Artifacts.CollectorModule
{
    class CM_Profit : ArtifactProfit
    {
        public CM_Profit()
        {
            type = ParameterType.Inner;
            title = "МК: выгодность";
            details = "";
            fractionalDigits = 2;
            tags.Add(ParameterTag.items);
            tags.Add(ParameterTag.artifacts);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            var module = calculator.UpdatedModule<CM_CalculationModule>();
            value = unroundValue = module.profit;

            return calculationReport;
        }
    }
}
