﻿using System;
using System.Collections.Generic;
using System.Linq;

using ModelAnalyzer.Services;

namespace ModelAnalyzer.Parameters.Items.Artifacts.HoleBox
{
    class HB_TensionIncreasing : FloatArrayParameter
    {
        public HB_TensionIncreasing()
        {
            type = ParameterType.Out;
            title = "ДК: изменение напряженности";
            details = "Массив определяющий изменение напряженности в зависимости от текущей ступени. Первая ступень всегда описывает скорость снижения напряженности при нулевом антизаряде.";
            fractionalDigits = 0;
            tags.Add(ParameterTag.items);
            tags.Add(ParameterTag.artifacts);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float tisa = RequestParmeter<HB_TensionInreasingStepsAmount>(calculator).GetValue();

            if (!calculationReport.IsSuccess)
                return calculationReport;
            ClearValues();

            for (int i = 0; i < tisa; i++)
            {
                var increasing = (float)Math.Pow(2, i);
                unroundValues.Add(increasing);
            }

            var unroundDecreasing = -unroundValues.Average();
            var decreasing = (float)Math.Round(unroundDecreasing, MidpointRounding.AwayFromZero);

            values.AddRange(unroundValues);

            unroundValues.Insert(0, unroundDecreasing);
            values.Insert(0, decreasing);

            return calculationReport;
        }

        internal override ParameterValidationReport Validate(Validator validator, Storage storage)
        {
            var report = base.Validate(validator, storage);
            var tisa = storage.Parameter<HB_TensionInreasingStepsAmount>().GetValue();
            ValidateSize(tisa + 1, report);
            return report;
        }
    }
}
