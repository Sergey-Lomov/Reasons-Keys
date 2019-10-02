using System.Linq;

using ModelAnalyzer.Services;
using ModelAnalyzer.Parameters.Activities;
using ModelAnalyzer.Parameters.Events;
using ModelAnalyzer.Parameters.Events.Weight;

namespace ModelAnalyzer.Parameters.Items.Artifacts.LachesisNeedle
{
    class LN_OneConnectionProfit : FloatSingleParameter
    {
        public LN_OneConnectionProfit()
        {
            type = ParameterType.Inner;
            title = "ИЛ: выгодность установки одной связи";
            details = "";
            fractionalDigits = 2;
            tags.Add(ParameterTag.items);
            tags.Add(ParameterTag.artifacts);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float asl = calculator.UpdatedSingleValue(typeof(AverageSequenceLength));
            float asi = calculator.UpdatedSingleValue(typeof(AverageStabilityIncrement));
            float eifp = calculator.UpdatedSingleValue(typeof(EventImpactPrice));
            float eapr = calculator.UpdatedSingleValue(typeof(EstimatedArtifactsProfit));
            float brw = calculator.UpdatedSingleValue(typeof(BaseRelationsWeight));
            float frw = calculator.UpdatedSingleValue(typeof(FrontReasonsWeight));
            float fbw = calculator.UpdatedSingleValue(typeof(FrontBlockerWeight));

            float mrw = new float[] { brw, frw, fbw }.Max();

            value = unroundValue = mrw * asl * asi * eifp;

            return calculationReport;
        }
    }
}
