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

            float asl = calculator.UpdatedParameter<AverageSequenceLength>().GetValue();
            float asi = calculator.UpdatedParameter<AverageStabilityIncrement>().GetValue();
            float eifp = calculator.UpdatedParameter<EventImpactPrice>().GetValue();
            float eapr = calculator.UpdatedParameter<EstimatedArtifactsProfit>().GetValue();
            float brw = calculator.UpdatedParameter<BaseRelationsWeight>().GetValue();
            float frw = calculator.UpdatedParameter<FrontReasonsWeight>().GetValue();
            float fbw = calculator.UpdatedParameter<FrontBlockerWeight>().GetValue();

            float mrw = new float[] { brw, frw, fbw }.Max();

            value = unroundValue = mrw * asl * asi * eifp;

            return calculationReport;
        }
    }
}
