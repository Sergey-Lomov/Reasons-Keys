using System;

using ModelAnalyzer.Services;

namespace ModelAnalyzer.Parameters.Items.Artifacts.CoagulationGenerator
{
    class CG_Profit : ArtifactProfit
    {
        public CG_Profit()
        {
            type = ParameterType.Inner;
            title = "ГС: выгодность";
            details = "";
            fractionalDigits = 2;
            tags.Add(ParameterTag.items);
            tags.Add(ParameterTag.artifacts);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float oupr = RequestParameter<CG_OneUsageProfit>(calculator).GetValue();
            float ca = RequestParameter<CG_ChargesAmount>(calculator).GetValue();

            value = unroundValue = oupr * ca;

            return calculationReport;
        }
    }
}
