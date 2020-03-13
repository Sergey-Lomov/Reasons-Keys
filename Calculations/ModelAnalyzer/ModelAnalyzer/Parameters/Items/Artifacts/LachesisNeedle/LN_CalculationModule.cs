using System;
using System.Collections.Generic;
using System.Linq;

using ModelAnalyzer.Services;
using ModelAnalyzer.Parameters.General;
using ModelAnalyzer.Parameters.Timing;
using ModelAnalyzer.Parameters.Topology;
using ModelAnalyzer.Parameters.Activities;
using ModelAnalyzer.Parameters.Events;
using ModelAnalyzer.Parameters.Events.Weight;
using ModelAnalyzer.Parameters.Items.Artifacts;

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

            float cna = RequestParmeter<ContinuumNodesAmount>(calculator, report).GetValue();
            float csl = RequestParmeter<ChainStabilityLimit>(calculator, report).GetValue();
            float eip = RequestParmeter<EventImpactPrice>(calculator, report).GetValue();
            float ra = RequestParmeter<RoundAmount>(calculator, report).GetValue();
            float aar = RequestParmeter<ArtifactsAvailabilityRound>(calculator, report).GetValue();
            float nkeca = RequestParmeter<NokeyEventCreationAmount>(calculator, report).GetValue();
            float frwc = RequestParmeter<FrontRelationsWeightCoef>(calculator, report).GetValue();
            float minpa = RequestParmeter<MinPlayersAmount>(calculator, report).GetValue();
            float maxpa = RequestParmeter<MaxPlayersAmount>(calculator, report).GetValue();
            float eapr = RequestParmeter<EstimatedArtifactsProfit>(calculator, report).GetValue();
            List<float> mdpa = RequestParmeter<MinDistancesPairsAmount_AA>(calculator, report).GetValue();

            if (!report.IsSuccess)
                return report;

            float fea = (minpa + maxpa) / 2 * nkeca;
            float vea = fea * (1 + aar / ra) / 2;
            float ncsc = 1 / csl;

            int range = minRange;
            float oupr = 0;

            int best_range = 1;
            float best_oupr = 0;
            int best_ua = 0;

            while (range <= maxRange && oupr < eapr * 2)
            {
                float validPairsSum = 0;
                for (int i = 0; i < range; i++)
                    validPairsSum += mdpa[i];

                float rc = validPairsSum / mdpa.Sum();
                float nec = rc * vea * ncsc;

                oupr = (csl - 1) * eip * frwc * nec;
                int ua = (int)Math.Round(eapr / oupr, MidpointRounding.AwayFromZero);
                if (Math.Abs(eapr - oupr * ua) < Math.Abs(eapr - best_oupr * best_ua))
                {
                    best_range = range;
                    best_ua = ua;
                    best_oupr = oupr;
                }

                range++;
            }

            this.range = best_range;
            this.connectinosAmount = best_ua;
            this.oneUsageProfit = best_oupr;

            return report;
        }
    }
}
