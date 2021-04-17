using System.Collections.Generic;

using ModelAnalyzer.Services;
using ModelAnalyzer.Parameters.Timing;

namespace ModelAnalyzer.Parameters.Items
{
    class ArtifactsAvailabilityRound : FloatSingleParameter
    {
        public ArtifactsAvailabilityRound()
        {
            type = ParameterType.Out;
            title = "Раунд доступности артефактов";
            details = "Раунд, начиная с которого артефакты могут войти в игру";
            fractionalDigits = 0;
            tags.Add(ParameterTag.items);
            tags.Add(ParameterTag.artifacts);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float aap = RequestParmeter<ArtifactsAvaliabilityPhase>(calculator).GetValue();
            List<float> pd = RequestParmeter<PhasesDuration>(calculator).GetValue();

            if (!calculationReport.IsSuccess)
                return calculationReport;

            int sum = 0;
            for (int i = 0; i < aap; i++)
                sum += (int)pd[i];

            sum++; //Because phases numeration starts by 0, but rounds numeration starts by 1

            value = unroundValue = sum;

            return calculationReport;
        }
    }
}
