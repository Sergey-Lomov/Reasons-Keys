using System;

using ModelAnalyzer.Services;
using ModelAnalyzer.Parameters.Events;

namespace ModelAnalyzer.Parameters.Items.Standard.RelationsImprover
{
    class RI_ImprovementLimit : FloatSingleParameter
    {
        public RI_ImprovementLimit()
        {
            type = ParameterType.Out;
            title = "УС: предел усиления";
            details = "";
            fractionalDigits = 0;
            tags.Add(ParameterTag.items);
            tags.Add(ParameterTag.baseItems);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float rip = RequestParmeter<RelationImpactPower>(calculator).GetValue();
            float ilc = RequestParmeter<RI_ImprovementLimitCoefficient>(calculator).GetValue();

            if (!calculationReport.IsSuccess)
                return calculationReport;

            unroundValue = (ilc + 1) * rip;
            value = (float)Math.Round(unroundValue, MidpointRounding.AwayFromZero);

            return calculationReport;
        }
    }
}
