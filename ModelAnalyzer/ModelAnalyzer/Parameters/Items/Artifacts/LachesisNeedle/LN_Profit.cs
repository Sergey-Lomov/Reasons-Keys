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

            float ca = calculator.UpdatedSingleValue(typeof(LN_ConnectionsAmount));
            float ocpr = calculator.UpdatedSingleValue(typeof(LN_OneConnectionProfit));

            value = unroundValue = ca * ocpr;

            return calculationReport;
        }
    }
}
