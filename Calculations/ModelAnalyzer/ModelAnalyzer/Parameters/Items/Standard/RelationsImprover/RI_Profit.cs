using ModelAnalyzer.Services;
using ModelAnalyzer.Parameters.Activities;

namespace ModelAnalyzer.Parameters.Items.Standard.RelationsImprover
{
    class RI_Profit : FloatSingleParameter
    {
        public RI_Profit()
        {
            type = ParameterType.Inner;
            title = "УС: выгодность";
            details = "";
            fractionalDigits = 2;
            tags.Add(ParameterTag.items);
            tags.Add(ParameterTag.baseItems);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float eip = RequestParameter<EventImpactPrice>(calculator).GetValue();
            float ca = RequestParameter<RI_ChargesAmount>(calculator).GetValue();

            if (!calculationReport.IsSuccess)
                return calculationReport;

            value = unroundValue = eip * ca;

            return calculationReport;
        }
    }
}
