using System;
using System.Linq;
using System.Collections.Generic;

using ModelAnalyzer.Services;
using ModelAnalyzer.Parameters.Mining;
using ModelAnalyzer.Parameters.Events;

namespace ModelAnalyzer.Parameters.Items.Artifacts.CoagulationGenerator
{
    class CG_V616Mining : FloatSingleParameter
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

            List<float> ma = RequestParameter<MiningAllocation>(calculator).GetValue();
            var anmb = RequestParameter<AverageNozeroMiningBonus>(calculator).GetValue();

            if (!calculationReport.IsSuccess)
                return calculationReport;

            unroundValue = ma[ma.Count - 1] + anmb;
            value = (float)Math.Round(unroundValue, MidpointRounding.AwayFromZero);

            return calculationReport;
        }
    }
}
