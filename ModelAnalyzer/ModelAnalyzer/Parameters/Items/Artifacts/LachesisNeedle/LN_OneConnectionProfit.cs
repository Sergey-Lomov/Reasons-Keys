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

            float asl = RequestParmeter<AverageSequenceLength>(calculator).GetValue();
            float asi = RequestParmeter<AverageStabilityIncrement>(calculator).GetValue();
            float eifp = RequestParmeter<EventImpactPrice>(calculator).GetValue();
            float eapr = RequestParmeter<EstimatedArtifactsProfit>(calculator).GetValue();
            float brw = RequestParmeter<BaseRelationsWeight>(calculator).GetValue();
            float frw = RequestParmeter<FrontReasonsWeight>(calculator).GetValue();
            float fbw = RequestParmeter<FrontBlockerWeight>(calculator).GetValue();

            float mrw = new float[] { brw, frw, fbw }.Max();

            value = unroundValue = mrw * asl * asi * eifp;

            return calculationReport;
        }
    }
}
