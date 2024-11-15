﻿using System;
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
            ignoreRoundingIssue = true;
            tags.Add(ParameterTag.items);
            tags.Add(ParameterTag.artifacts);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float tisa = RequestParameter<HB_TensionInreasingStepsAmount>(calculator).GetValue();
            List<float> tl = RequestParameter<HB_TensionLimits>(calculator).GetValue();

            if (!calculationReport.IsSuccess)
                return calculationReport;
            
            float maeu = tl.Last();

            unroundValue = maeu / tisa;
            value = (float)Math.Floor(unroundValue);

            return calculationReport;
        }
    }
}
