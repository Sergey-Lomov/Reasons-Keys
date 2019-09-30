using System;

using ModelAnalyzer.Parameters.Mining;
using ModelAnalyzer.Parameters.Events;

namespace ModelAnalyzer.Parameters.Items.Artifacts.CoagulationGenerator
{
    class CG_V616Mining : SingleParameter
    {
        public CG_V616Mining()
        {
            type = ParameterType.Out;
            title = "ГС: V616 добыча";
            details = "Добыча ТЗ в специальном узле V616";
            fractionalDigits = 0;
            tags.Add(ParameterTag.items);
            tags.Add(ParameterTag.artifacts);
            tags.Add(ParameterTag.mining);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float amb = calculator.UpdatedSingleValue(typeof(AverageMiningBonus));
            float[] ma = calculator.UpdatedArrayValue(typeof(MiningAllocation));

            unroundValue = ma[ma.Length - 1] + amb;
            value = (float)Math.Round(unroundValue, MidpointRounding.AwayFromZero);

            return calculationReport;
        }
    }
}
