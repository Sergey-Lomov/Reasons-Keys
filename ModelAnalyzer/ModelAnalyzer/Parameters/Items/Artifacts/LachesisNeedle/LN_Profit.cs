using ModelAnalyzer.Services;

namespace ModelAnalyzer.Parameters.Items.Artifacts.LachesisNeedle
{
    class LN_Profit : FloatSingleParameter
    {
        public LN_Profit()
        {
            type = ParameterType.Inner;
            title = "ИЛ: выгодность";
            details = "";
            fractionalDigits = 2;
            tags.Add(ParameterTag.items);
            tags.Add(ParameterTag.artifacts);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float ca = calculator.UpdatedParameter<LN_ConnectionsAmount>().GetValue();
            float ocpr = calculator.UpdatedParameter<LN_OneConnectionProfit>().GetValue();

            value = unroundValue = ca * ocpr;

            return calculationReport;
        }
    }
}
