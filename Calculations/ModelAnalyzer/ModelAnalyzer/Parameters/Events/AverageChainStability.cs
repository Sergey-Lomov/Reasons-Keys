using ModelAnalyzer.Services;

namespace ModelAnalyzer.Parameters.Events
{
    class AverageChainStability : FloatSingleParameter
    {
        public AverageChainStability()
        {
            type = ParameterType.Inner;
            title = "Cредняя цепная стабильность";
            details = "";
            fractionalDigits = 2;
            tags.Add(ParameterTag.events);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float csl = RequestParmeter<ChainStabilityLimit>(calculator).GetValue();

            if (!calculationReport.IsSuccess)
                return calculationReport;

            value = unroundValue = (csl + 1) / 2;

            return calculationReport;
        }
    }
}
