using ModelAnalyzer.Services;
using ModelAnalyzer.Parameters.BranchPoints;

namespace ModelAnalyzer.Parameters.Items.Artifacts.FateRavel
{
    class FR_Profit : ArtifactProfit
    {
        public FR_Profit()
        {
            type = ParameterType.Inner;
            title = "КС: выгодность";
            details = "";
            fractionalDigits = 2;
            tags.Add(ParameterTag.items);
            tags.Add(ParameterTag.artifacts);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float ob = RequestParmeter<FR_OwnerBets>(calculator).GetValue();
            float bppr = RequestParmeter<BranchPointProfit>(calculator).GetValue();
            float eprc = RequestParmeter<EstimatedPrognosedRealisationChance>(calculator).GetValue();

            if (!calculationReport.IsSuccess)
                return calculationReport;

            value = unroundValue = (ob * eprc - 0.5f) * bppr;

            return calculationReport;
        }
    }
}
