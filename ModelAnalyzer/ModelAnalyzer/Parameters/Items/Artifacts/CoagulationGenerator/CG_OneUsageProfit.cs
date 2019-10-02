using System;
using System.Collections.Generic;
using System.Linq;

using ModelAnalyzer.Services;
using ModelAnalyzer.Parameters.Topology;
using ModelAnalyzer.Parameters.Moving;
using ModelAnalyzer.Parameters.Items.Standard.SpeedBooster;

namespace ModelAnalyzer.Parameters.Items.Artifacts.CoagulationGenerator
{
    class CG_OneUsageProfit : FloatSingleParameter
    {
        public CG_OneUsageProfit()
        {
            type = ParameterType.Inner;
            title = "ГС: выгодность одного использования";
            details = "Выгодность разового применения генератор вертывания";
            fractionalDigits = 2;
            tags.Add(ParameterTag.items);
            tags.Add(ParameterTag.artifacts);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float isp = calculator.UpdatedSingleValue(typeof(InitialSpeed));
            float mp = calculator.UpdatedSingleValue(typeof(MotionPrice));
            float orsdpr = calculator.UpdatedSingleValue(typeof(OneRoundSpeedDoublingProfit));
            float[] mdpa = calculator.UpdatedArrayValue(typeof(MinDistancesPairsAmount));
            float[] sbp = calculator.UpdatedArrayValue(typeof(SB_Power));

            float md = mdpa.Length;

            var um = new List<float>();
            for (int i = 1; i <= md; i++)
            {
                var motivation = ((float)i - 1) / (md - 1);
                um.Add(motivation);
            }

            var leftValues = new List<float>();
            var rightValues = new List<float>();
            for (int i = 1; i <= md; i++)
            {
                var leftValue = i * mdpa[i - 1] * um[i - 1];
                leftValues.Add(leftValue);
                var rightValue = mdpa[i - 1] * um[i - 1];
                rightValues.Add(rightValue);
            }

            var moud = leftValues.Sum() / rightValues.Sum();
            var asp = isp + sbp.Sum() / 2;
            var sspr = (float)Math.Pow(orsdpr, Math.Log(moud / asp, 2));

            value = unroundValue = (moud - 1) * mp + sspr;

            return calculationReport;
        }
    }
}
