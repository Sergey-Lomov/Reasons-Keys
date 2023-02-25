using System;

using ModelAnalyzer.Services;
using ModelAnalyzer.Parameters.Items;

namespace ModelAnalyzer.Parameters.Activities
{
    class ArtifactsDischargeCompensation : FloatSingleParameter
    {
        public ArtifactsDischargeCompensation()
        {
            type = ParameterType.Out;
            title = "Компенсация при разрядке артефактов";
            details = "";
            fractionalDigits = 0;
            tags.Add(ParameterTag.activities);
            tags.Add(ParameterTag.artifacts);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            var eap = RequestParameter<EstimatedArtifactsProfit>(calculator).GetValue();
            var peupc = RequestParameter<PureEUProfitCoefficient>(calculator).GetValue();
            var adc = RequestParameter<ArtifactDischargeCoef>(calculator).GetValue();

            if (!calculationReport.IsSuccess)
                return calculationReport;

            unroundValue = adc * eap / peupc;
            value = (float)Math.Round(unroundValue, MidpointRounding.AwayFromZero);

            return calculationReport;
        }
    }
}
