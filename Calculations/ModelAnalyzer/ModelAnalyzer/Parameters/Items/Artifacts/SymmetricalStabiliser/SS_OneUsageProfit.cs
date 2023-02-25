using ModelAnalyzer.Services;

namespace ModelAnalyzer.Parameters.Items.Artifacts.SymmetricalStabiliser
{
    class SS_OneUsageProfit : FloatSingleParameter
    {
        public SS_OneUsageProfit()
        {
            type = ParameterType.Inner;
            title = "СС: выгодность одного использования";
            details = "";
            fractionalDigits = 2;
            tags.Add(ParameterTag.items);
            tags.Add(ParameterTag.artifacts);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float ssp = RequestParameter<SS_Profit>(calculator).GetValue();
            float ssua = RequestParameter<SS_UsageAmount>(calculator).GetValue();

            if (!calculationReport.IsSuccess)
                return calculationReport;

            value = unroundValue = ssp / ssua;

            return calculationReport;
        }
    }
}
