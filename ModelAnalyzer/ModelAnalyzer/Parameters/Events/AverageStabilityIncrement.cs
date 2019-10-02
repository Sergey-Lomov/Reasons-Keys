using System.Linq;

using ModelAnalyzer.Services;

namespace ModelAnalyzer.Parameters.Events
{
    class AverageStabilityIncrement : FloatSingleParameter
    {
        public AverageStabilityIncrement()
        {
            type = ParameterType.Inner;
            title = "Средний прирост стабильности события";
            details = "";
            fractionalDigits = 2;
            tags.Add(ParameterTag.events);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float cna = calculator.UpdatedSingleValue(typeof(ContinuumNodesAmount));
            float[] sia = calculator.UpdatedArrayValue(typeof(StabilityIncrementAllocation));

            float average = 0;
            for (int i = 0; i < sia.Count(); i++)
                average += i * sia[i] / sia.Sum();

            value = unroundValue = average;

            return calculationReport;
        }
    }
}
