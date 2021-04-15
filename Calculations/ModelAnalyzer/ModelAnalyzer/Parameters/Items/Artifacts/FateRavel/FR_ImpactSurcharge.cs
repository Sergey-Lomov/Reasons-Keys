using ModelAnalyzer.Services;
using ModelAnalyzer.Parameters.Activities;

namespace ModelAnalyzer.Parameters.Items.Artifacts.FateRavel
{
    class FR_ImpactSurcharge : FloatSingleParameter
    {
        public FR_ImpactSurcharge()
        {
            type = ParameterType.Out;
            title = "КС: доплата воздействия";
            details = "Дополнительный взнос при вкладе ТЗ в ставку";
            fractionalDigits = 0;
            tags.Add(ParameterTag.items);
            tags.Add(ParameterTag.artifacts);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            int eip = (int)RequestParmeter<EventImpactPrice>(calculator).GetValue();

            if (!calculationReport.IsSuccess)
                return calculationReport;

            value = unroundValue = eip - 1;

            return calculationReport;
        }
    }
}
