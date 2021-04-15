using System;

using ModelAnalyzer.Services;
using ModelAnalyzer.Parameters.BranchPoints;

namespace ModelAnalyzer.Parameters.Items.Artifacts.FateRavel
{
    class FR_OwnerBets : FloatSingleParameter
    {
        public FR_OwnerBets()
        {
            type = ParameterType.Out;
            title = "КС: ставки организатора";
            details = "";
            fractionalDigits = 0;
            tags.Add(ParameterTag.items);
            tags.Add(ParameterTag.artifacts);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float bppr = RequestParmeter<BranchPointProfit>(calculator).GetValue();
            float eprc = RequestParmeter<EstimatedPrognosedRealisationChance>(calculator).GetValue();
            float eapr = RequestParmeter<EstimatedArtifactsProfit>(calculator).GetValue();

            if (!calculationReport.IsSuccess)
                return calculationReport;

            unroundValue = (eapr / bppr + 0.5f) / eprc;
            value = (float)Math.Round(unroundValue, MidpointRounding.AwayFromZero);

            return calculationReport;
        }
    }
}
