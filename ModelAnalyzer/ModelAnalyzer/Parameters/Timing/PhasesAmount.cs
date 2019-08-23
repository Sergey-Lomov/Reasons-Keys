using ModelAnalyzer.Parameters.Topology;

namespace ModelAnalyzer.Parameters.Timing
{
    class PhasesAmount : SingleParameter
    {
        public PhasesAmount()
        {
            type = ParameterType.Inner;
            title = "Кол-во фаз";
            details = "Всегда на 1 больше, чем радиус поля";
            fractionalDigits = 0;
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float r = calculator.UpdatedSingleValue(typeof(FieldRadius));
            value = unroundValue = r + 1;

            return calculationReport;
        }
    }
}
