using System;
using System.Collections.Generic;

using ModelAnalyzer.Parameters.Topology;
using ModelAnalyzer.Parameters.Moving;
using ModelAnalyzer.Parameters.Timing;

namespace ModelAnalyzer.Parameters.Mining
{
    class CenterMiningBonus : SingleParameter
    {
        private readonly string phaseDurationIssue = "Не удалось получить длительность 0-фазы";
        private readonly string miningAllocationIssue = "Массив \"{0}\" содержит меньше элементов чем, \"{1}\" + 1. Невозможно получить значение на максимальном радиусе.";

        public CenterMiningBonus()
        {
            type = ParameterType.Out;
            title = "Бонус добычи центрального узла";
            details = "Бонус добычи центрального узла, обеспечивающий соблюдение баланса 0-фазы (подробнее в механике).";
            fractionalDigits = 0;
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float mp = calculator.UpdatedSingleValue(typeof(MotionPrice));
            float isp = calculator.UpdatedSingleValue(typeof(InitialSpeed));
            float fr = calculator.UpdatedSingleValue(typeof(FieldRadius));
            float au = calculator.UpdatedSingleValue(typeof(AUMoveAmount));
            float[] pd = calculator.UpdatedArrayValue(typeof(PhasesDuration));
            float[] ma = calculator.UpdatedArrayValue(typeof(MiningAllocation));

            // Check values
            var invalidTitles = new List<string>();
            
            if (float.IsNaN(mp))
                invalidTitles.Add(calculator.ParameterTitle(typeof(MotionPrice)));

            if (float.IsNaN(isp))
                invalidTitles.Add(calculator.ParameterTitle(typeof(InitialSpeed)));

            if (float.IsNaN(au))
                invalidTitles.Add(calculator.ParameterTitle(typeof(AUMoveAmount)));

            if (ma.Length == 0)
                invalidTitles.Add(calculator.ParameterTitle(typeof(MiningAllocation)));

            if (invalidTitles.Count > 0)
            {
                FailCalculationByInvalidIn(invalidTitles.ToArray());
                return calculationReport;
            }

            var issues = new List<string>();
            if (pd.Length < 1)
            {
                issues.Add(phaseDurationIssue);
            }
            if (ma.Length < fr + 1)
            {
                var maTitle = calculator.ParameterTitle(typeof(MiningAllocation));
                var frTitle = calculator.ParameterTitle(typeof(FieldRadius));
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
