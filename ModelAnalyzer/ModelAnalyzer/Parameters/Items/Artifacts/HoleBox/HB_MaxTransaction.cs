using System;
using System.Linq;
using System.Collections.Generic;

using ModelAnalyzer.Services;

namespace ModelAnalyzer.Parameters.Items.Artifacts.HoleBox
{
    class HB_MaxTransaction : FloatSingleParameter
    {
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

            float tisa = RequestParmeter<HB_TensionInreasingStepsAmount>(calculator).GetValue();
            List<float> tl = RequestParmeter<HB_TensionLimits>(calculator).GetValue();

            if (!calculationReport.IsSuccess)
                return calculationReport;
            
            float maeu = tl.Last();

            unroundValue = maeu / (tisa + 1);
            value = (float)Math.Ceiling(unroundValue);

            return calculationReport;
        }
    }
}
