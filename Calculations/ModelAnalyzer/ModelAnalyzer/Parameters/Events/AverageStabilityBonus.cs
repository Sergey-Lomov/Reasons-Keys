using System.Collections.Generic;
using System.Linq;

using ModelAnalyzer.Services;
using ModelAnalyzer.Parameters.Topology;

namespace ModelAnalyzer.Parameters.Events
{
    class AverageStabilityBonus : FloatSingleParameter
    {
        public AverageStabilityBonus()
        {
            type = ParameterType.Inner;
            title = "Средний бонус стабильности";
            details = "";
            fractionalDigits = 2;
            tags.Add(ParameterTag.events);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float cna = RequestParmeter<ContinuumNodesAmount>(calculator).GetValue();
            List<float> sia = RequestParmeter<StabilityBonusAllocation>(calculator).GetValue(); 

            float average = 0;
            for (int i = 0; i < sia.Count(); i++)
                average += i * sia[i] / sia.Sum();

            value = unroundValue = average;

            return calculationReport;
        }
    }
}
