using System;
using System.Collections.Generic;

using ModelAnalyzer.Services;
using ModelAnalyzer.Parameters.Timing;

namespace ModelAnalyzer.Parameters.Items.Artifacts.HoleBox
{
    class HB_TensionLimits : FloatArrayParameter
    {
        public HB_TensionLimits()
        {
            type = ParameterType.Out;
            title = "ДК: пороги изменения напряженности";
            details = "Массив определяющий максимальные границы ступеней прироста напряжения. Последнее его значение задает максимально возможный антизаряд, а первое всегда равно нулю - ситуации в которой игрок не занимал либо полностью вернул ТЗ.";
            fractionalDigits = 0;
            tags.Add(ParameterTag.items);
            tags.Add(ParameterTag.artifacts);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float eapr = RequestParmeter<EstimatedArtifactsProfit>(calculator).GetValue();
            float peuprc = RequestParmeter<PureEUProfitCoefficient>(calculator).GetValue();
            float tisa = RequestParmeter<HB_TensionInreasingStepsAmount>(calculator).GetValue();

            if (!calculationReport.IsSuccess)
                return calculationReport;
            ClearValues();

            float unroundMaeu = eapr / peuprc;
            float maeu = (float)Math.Round(unroundMaeu, MidpointRounding.AwayFromZero);

            for (int i = 0; i <= tisa; i++)
                unroundValues.Add(i * maeu / tisa);

            float roundingCredit = 0;
            foreach (var unroundValue in unroundValues)
            {
                var value = (float)Math.Round(unroundValue + roundingCredit, MidpointRounding.AwayFromZero);
                roundingCredit += unroundValue - value;
                values.Add(value);
            }

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
