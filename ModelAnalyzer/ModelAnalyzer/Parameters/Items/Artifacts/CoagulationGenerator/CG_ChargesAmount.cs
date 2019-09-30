using System;

namespace ModelAnalyzer.Parameters.Items.Artifacts.CoagulationGenerator
{
    class CG_ChargesAmount : SingleParameter
    {
        public CG_ChargesAmount()
        {
            type = ParameterType.Out;
            title = "ГС: кол-во зарядов";
            details = "";
            fractionalDigits = 0;
            tags.Add(ParameterTag.items);
            tags.Add(ParameterTag.artifacts);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float eapr = calculator.UpdatedSingleValue(typeof(EstimatedArtifactsProfit));
            float oupr = calculator.UpdatedSingleValue(typeof(CG_OneUsageProfit));

            unroundValue = eapr / oupr;
            value = (float)Math.Round(unroundValue, MidpointRounding.AwayFromZero);

            return calculationReport;
        }
    }
}
