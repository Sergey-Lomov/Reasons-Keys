using ModelAnalyzer.Services;

namespace ModelAnalyzer.Parameters.Items.Artifacts.LachesisNeedle
{
    class LN_OneConnectionProfit : FloatSingleParameter
    {
        public LN_OneConnectionProfit()
        {
            type = ParameterType.Inner;
            title = "ИЛ: выгодность установки одной связи";
            details = "";
            fractionalDigits = 2;
            tags.Add(ParameterTag.items);
            tags.Add(ParameterTag.artifacts);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            var cm = calculator.UpdatedModule<LN_CalculationModule>();
            value = unroundValue = cm.oneUsageProfit;

            return calculationReport;
        }
    }
}
