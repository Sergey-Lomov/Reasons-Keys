using ModelAnalyzer.Services;

namespace ModelAnalyzer.Parameters.Items.Artifacts
{
    class ArtifactInaccessibilityCompensation : FloatSingleParameter
    {
        public ArtifactInaccessibilityCompensation()
        {
            type = ParameterType.Out;
            title = "Компенсация недоступности артефакта";
            details = "";
            fractionalDigits = 0;
            tags.Add(ParameterTag.items);
            tags.Add(ParameterTag.artifacts);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float eap = RequestParmeter<EstimatedArtifactsProfit>(calculator).GetValue();
            float peupc = RequestParmeter<PureEUProfitCoefficient>(calculator).GetValue();

            if (!calculationReport.IsSuccess)
                return calculationReport;

            unroundValue = eap / peupc;
            value = (float)System.Math.Round(unroundValue);

            return calculationReport;
        }
    }
}
