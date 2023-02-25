using System;

using ModelAnalyzer.Services;
using ModelAnalyzer.Parameters.Items.Artifacts.CoagulationGenerator;
using ModelAnalyzer.Parameters.Items.Artifacts.CollectorModule;
using ModelAnalyzer.Parameters.Items.Artifacts.FateRavel;
using ModelAnalyzer.Parameters.Items.Artifacts.HoleBox;
using ModelAnalyzer.Parameters.Items.Artifacts.LachesisNeedle;
using ModelAnalyzer.Parameters.Items.Artifacts.SymmetricalStabiliser;

namespace ModelAnalyzer.Parameters.Predictability
{
    class RoundMaxProfit : FloatSingleParameter
    {
        public RoundMaxProfit()
        {
            type = ParameterType.Inner;
            title = "Максимальная выгодность хода";
            details = "Максимальная выгода, которой игрок может добиться при идеальной подготовке и удаче в течении обычного хода";
            fractionalDigits = 2;
            tags.Add(ParameterTag.predictability);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float aarp = RequestParmeter<AverageActrionsRoundProfit>(calculator).GetValue();
            float mbirp = RequestParmeter<MaxBaseItemsRoundPureProfit>(calculator).GetValue();
            float cgup = RequestParmeter<CG_OneUsageProfit>(calculator).GetValue();
            float cmup = RequestParmeter<CM_OneUsageProfit>(calculator).GetValue();
            float frp = RequestParmeter<FR_Profit>(calculator).GetValue();
            float hbeup = RequestParmeter<HB_EstimatedOneUsageProfit>(calculator).GetValue();
            float lnocp = RequestParmeter<LN_OneConnectionProfit>(calculator).GetValue();
            float ssup = RequestParmeter<SS_OneUsageProfit>(calculator).GetValue();

            if (!calculationReport.IsSuccess)
                return calculationReport;

            float maxMovement = cgup;
            float maxEnergy = Math.Max(cmup, hbeup);
            float maxEvent = Math.Max(ssup, Math.Max(frp, lnocp));

            value = aarp + mbirp + maxMovement + maxEnergy + maxEvent;

            return calculationReport;
        }
    }
}
