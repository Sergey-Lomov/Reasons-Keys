using ModelAnalyzer.Services;

namespace ModelAnalyzer.Parameters.Events
{
    class AverageEventStability : FloatSingleParameter
    {
        public AverageEventStability()
        {
            type = ParameterType.Inner;
            title = "Средняя стабильность события";
            details = "Средняя стабильность события, с учетом цепной и бонусной стабильностей";
            fractionalDigits = 2;
            tags.Add(ParameterTag.events);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float abs = RequestParmeter<AverageStabilityIncrement>(calculator).GetValue();
            float acs = RequestParmeter<AverageChainStability>(calculator).GetValue();

            if (!calculationReport.IsSuccess)
                return calculationReport;

            value = unroundValue = acs + abs;

            return calculationReport;
        }
    }
}
