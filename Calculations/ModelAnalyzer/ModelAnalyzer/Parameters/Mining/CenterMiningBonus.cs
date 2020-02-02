using System;
using System.Collections.Generic;

using ModelAnalyzer.Services;
using ModelAnalyzer.Parameters.Topology;
using ModelAnalyzer.Parameters.Moving;
using ModelAnalyzer.Parameters.Timing;

namespace ModelAnalyzer.Parameters.Mining
{
    class CenterMiningBonus : FloatSingleParameter
    {
        private readonly string phaseDurationIssue = "Не удалось получить длительность 0-фазы";
        private readonly string miningAllocationIssue = "Массив \"{0}\" содержит меньше элементов чем, \"{1}\" + 1. Невозможно получить значение на максимальном радиусе.";

        public CenterMiningBonus()
        {
            type = ParameterType.Out;
            title = "Бонус добычи центрального узла";
            details = "Бонус добычи центрального узла, обеспечивающий соблюдение баланса 0-фазы (подробнее в механике).";
            fractionalDigits = 0;
            tags.Add(ParameterTag.mining);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float mp = RequestParmeter<MotionPrice>(calculator).GetValue();
            float isp = RequestParmeter<InitialSpeed>(calculator).GetValue();
            float fr = RequestParmeter<FieldRadius>(calculator).GetValue();
            float au = RequestParmeter<AUMoveAmount>(calculator).GetValue();
            List<float> pd = RequestParmeter<PhasesDuration>(calculator).GetValue();
            List<float> ma = RequestParmeter<MiningAllocation>(calculator).GetValue();

            if (!calculationReport.IsSuccess)
                return calculationReport;

            var issues = new List<string>();
            if (pd.Count < 1)
            {
                issues.Add(phaseDurationIssue);
            }
            if (ma.Count < fr + 1)
            {
                var maTitle = RequestParmeter<MiningAllocation>(calculator).title;
                var frTitle = RequestParmeter<FieldRadius>(calculator).title;
                var issue = string.Format(miningAllocationIssue, maTitle, frTitle);
                issues.Add(issue);
            }

            if (issues.Count > 0)
            {
                calculationReport.Failed(issues);
                return calculationReport;
            }

            //Imitate player, which go to bigest radius as fast as possible and mine
            float profit = 0;
            for (int i = 1; i <= pd[0]; i++)
            {
                var finishRadius = Math.Min(fr, isp * i);
                var startRadius = Math.Min(fr, isp * (i - 1));
                var motionCost = (finishRadius - startRadius) * mp;
                var mining = ma[(int)finishRadius] * au;
                profit += mining - motionCost;
            }

            unroundValue = profit / (pd[0] * au);
            value = (float)Math.Round(unroundValue);

            return calculationReport;
        }
    }
}
