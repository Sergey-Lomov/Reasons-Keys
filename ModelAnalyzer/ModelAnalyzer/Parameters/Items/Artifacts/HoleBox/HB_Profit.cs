using System;
using System.Linq;
using System.Collections.Generic;

using ModelAnalyzer.Services;
using ModelAnalyzer.Parameters.Timing;

namespace ModelAnalyzer.Parameters.Items.Artifacts.HoleBox
{
    class HB_Profit : ArtifactProfit
    {
        private const string emptyArrayIssue = "Параметр {0} не содержит значений";

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

            float peuprc = calculator.UpdatedParameter<PureEUProfitCoefficient>().GetValue();
            float ra = calculator.UpdatedParameter<RoundAmount>().GetValue();
            float cpd = calculator.UpdatedParameter<HB_CollapsePreparationDuration>().GetValue();
            float ocac = calculator.UpdatedParameter<HB_OwnerCollapseAbsorbCoefficient>().GetValue();
            List<float> tl = calculator.UpdatedParameter<HB_TensionLimits>().GetValue();

            if (tl.Count() == 0)
            {
                var title = calculator.UpdatedParameter<HB_TensionLimits>().title;
                var mesasge = string.Format(emptyArrayIssue, title);
                calculationReport.Failed(mesasge);
                value = unroundValue = 0;
                return calculationReport;
            }

            float maeu = tl.Last();

            value = unroundValue = maeu * (peuprc + (1 - cpd / ra) * (1 - ocac) / 2);

            return calculationReport;
        }
    }
}
