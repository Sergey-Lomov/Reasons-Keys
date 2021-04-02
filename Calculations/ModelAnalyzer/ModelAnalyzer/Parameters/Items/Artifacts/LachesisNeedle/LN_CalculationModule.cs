using System;
using System.Collections.Generic;
using System.Linq;

using ModelAnalyzer.Services;
using ModelAnalyzer.Parameters.Activities;
using ModelAnalyzer.Parameters.Events;
using ModelAnalyzer.Parameters.Events.Weight;
using ModelAnalyzer.Parameters.Moving;

namespace ModelAnalyzer.Parameters.Items.Artifacts.LachesisNeedle
{
    class LN_CalculationModule : CalculationModule
    {
        internal float range;
        internal float oneUsageProfit;
        internal float connectinosAmount;

        private const int minRange = 2; //Min LN range for selection
        private const int maxRange = 8; //Max distance between two points

        public LN_CalculationModule()
        {
            title = "ИЛ: Модуль подбора";
        }

        internal override ModuleCalculationReport Execute(Calculator calculator)
        {
            var report = new ModuleCalculationReport(this);

            float eip = RequestParmeter<EventImpactPrice>(calculator, report).GetValue();
            float rip = RequestParmeter<RelationImpactPower>(calculator, report).GetValue();
            float frwc = RequestParmeter<FrontRelationsWeightCoef>(calculator, report).GetValue();
            float mp = RequestParmeter<MotionPrice>(calculator, report).GetValue();
            float eapr = RequestParmeter<EstimatedArtifactsProfit>(calculator, report).GetValue();
            int minr = (int)RequestParmeter<LN_MinRadius>(calculator, report).GetValue();
            int maxr = (int)RequestParmeter<LN_MaxRadius>(calculator, report).GetValue();

            if (!report.IsSuccess)
                return report;

            Func<int, float> unround_uc = r => eapr / (r * mp + eip * rip * frwc);
            Func<int, int> uc = r => (int)Math.Round(unround_uc(r), MidpointRounding.AwayFromZero);
            Func<int, float> apr = r => (r * mp + eip * rip * frwc) * uc(r);
            Func<int, float> delta = r => Math.Abs(eapr - apr(r));

            int best_r = minr;

            for (int r = minr; r <= maxr; r++)
                best_r = delta(r) < delta(best_r) ? r : best_r;

            range = best_r;
            connectinosAmount = uc(best_r);
            oneUsageProfit = best_r * mp + eip * rip * frwc;

            return report;
        }
    }
}
