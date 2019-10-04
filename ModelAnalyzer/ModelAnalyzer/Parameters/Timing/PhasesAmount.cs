using ModelAnalyzer.Services;
using ModelAnalyzer.Parameters.Topology;

namespace ModelAnalyzer.Parameters.Timing
{
    class PhasesAmount : FloatSingleParameter
    {
        public PhasesAmount()
        {
            type = ParameterType.Inner;
            title = "Кол-во фаз";
            details = "Всегда на 1 больше, чем радиус поля";
            fractionalDigits = 0;
            tags.Add(ParameterTag.timing);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float r = calculator.UpdatedParameter<FieldRadius>().GetValue();
            value = unroundValue = r + 1;

            return calculationReport;
        }
    }
}
