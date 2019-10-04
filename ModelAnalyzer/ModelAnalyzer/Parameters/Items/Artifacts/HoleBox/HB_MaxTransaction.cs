using System;
using System.Linq;
using System.Collections.Generic;

using ModelAnalyzer.Services;

namespace ModelAnalyzer.Parameters.Items.Artifacts.HoleBox
{
    class HB_MaxTransaction : FloatSingleParameter
    {
        private const string emptyArrayIssue = "Параметр {0} не содержит значений";

        public HB_MaxTransaction()
        {
            type = ParameterType.Out;
            title = "ДК: максимальная транзакция";
            details = "Ограничение на максимальное кол-во ТЗ, которое игрок может взять за 1 раз";
            fractionalDigits = 0;
            tags.Add(ParameterTag.items);
            tags.Add(ParameterTag.artifacts);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float tisa = calculator.UpdatedParameter<HB_TensionInreasingStepsAmount>().GetValue();
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

            unroundValue = maeu / (tisa + 1);
            value = (float)Math.Ceiling(unroundValue);

            return calculationReport;
        }
    }
}
