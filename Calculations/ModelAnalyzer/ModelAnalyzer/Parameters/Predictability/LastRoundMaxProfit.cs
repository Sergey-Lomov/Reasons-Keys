using System;

using ModelAnalyzer.Services;
using ModelAnalyzer.Parameters.Items.Artifacts.CoagulationGenerator;
using ModelAnalyzer.Parameters.Items.Artifacts.CollectorModule;
using ModelAnalyzer.Parameters.Items.Artifacts.HoleBox;

namespace ModelAnalyzer.Parameters.Predictability
{
    class LastRoundMaxProfit : FloatSingleParameter
    {
        public LastRoundMaxProfit()
        {
            type = ParameterType.Inner;
            title = "Максимальная выгодность последнего хода фазы";
            details = "Максимальная выгода, которой игрок может добиться при идеальной подготовке и удаче в течении последнего хода фазы";
            fractionalDigits = 2;
            tags.Add(ParameterTag.predictability);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float aarp = RequestParameter<AverageActrionsRoundProfit>(calculator).GetValue();
            float mbirp = RequestParameter<MaxBaseItemsRoundPureProfit>(calculator).GetValue();
            float cgup = RequestParameter<CG_OneUsageProfit>(calculator).GetValue();
            float cmup = RequestParameter<CM_OneUsageProfit>(calculator).GetValue();
            float hbeup = RequestParameter<HB_EstimatedOneUsageProfit>(calculator).GetValue();

            if (!calculationReport.IsSuccess)
                return calculationReport;

            float maxMovement = cgup;
            float maxEnergy = Math.Max(cmup, hbeup);

            value = aarp + mbirp + maxMovement + maxEnergy;

            return calculationReport;
        }
    }
}
