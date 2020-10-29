using System;
using System.Linq;
using System.Collections.Generic;

using ModelAnalyzer.Services;
using ModelAnalyzer.Parameters.Timing;

namespace ModelAnalyzer.Parameters.Items.Artifacts.HoleBox
{
    class HB_Profit : ArtifactProfit
    {
        public HB_Profit()
        {
            type = ParameterType.Inner;
            title = "ДК: выгодность";
            details = "";
            fractionalDigits = 2;
            tags.Add(ParameterTag.items);
            tags.Add(ParameterTag.artifacts);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            var peuprc = RequestParmeter<PureEUProfitCoefficient>(calculator).GetValue();
            var tl = RequestParmeter<HB_TensionLimits>(calculator).GetValue();

            if (!calculationReport.IsSuccess)
                return calculationReport;

            float maeu = tl.Last();
            value = unroundValue = maeu * peuprc;

            return calculationReport;
        }
    }
}
