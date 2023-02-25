using System.Linq;

using ModelAnalyzer.Services;
using ModelAnalyzer.Parameters.Items.Artifacts.CoagulationGenerator;
using ModelAnalyzer.Parameters.Items.Artifacts.CollectorModule;
using ModelAnalyzer.Parameters.Items.Artifacts.FateRavel;
using ModelAnalyzer.Parameters.Items.Artifacts.HoleBox;
using ModelAnalyzer.Parameters.Items.Artifacts.LachesisNeedle;
using ModelAnalyzer.Parameters.Items.Artifacts.SymmetricalStabiliser;

namespace ModelAnalyzer.Parameters.Items.Artifacts
{
    class AverageArtifactRoundProfit : FloatSingleParameter
    {
        public AverageArtifactRoundProfit()
        {
            type = ParameterType.Inner;
            title = "Средняя выгодность артефакта за ход";
            details = "Среднее арифметическое от выгодности одного применения каждого из артефактов.";
            fractionalDigits = 2;
            tags.Add(ParameterTag.items);
            tags.Add(ParameterTag.artifacts);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float cgup = RequestParameter<CG_OneUsageProfit>(calculator).GetValue();
            float cmup = RequestParameter<CM_OneUsageProfit>(calculator).GetValue();
            float frp = RequestParameter<FR_Profit>(calculator).GetValue();
            float hbeup = RequestParameter<HB_EstimatedOneUsageProfit>(calculator).GetValue();
            float lnocp = RequestParameter<LN_OneConnectionProfit>(calculator).GetValue();
            float ssup = RequestParameter<SS_OneUsageProfit>(calculator).GetValue();

            if (!calculationReport.IsSuccess)
                return calculationReport;

            float[] profits = {cgup, cmup, hbeup, frp, lnocp, ssup};
            value = unroundValue = profits.Average();

            return calculationReport;
        }
    }
}
