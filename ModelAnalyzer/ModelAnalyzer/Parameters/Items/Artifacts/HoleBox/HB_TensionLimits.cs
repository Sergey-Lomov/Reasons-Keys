using System;

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

            float eapr = calculator.UpdatedParameter<EstimatedArtifactsProfit>().GetValue();
            float peuprc = calculator.UpdatedParameter<PureEUProfitCoefficient>().GetValue();
            float ra = calculator.UpdatedParameter<RoundAmount>().GetValue();
            float cpd = calculator.UpdatedParameter<HB_CollapsePreparationDuration>().GetValue();
            float ocac = calculator.UpdatedParameter<HB_OwnerCollapseAbsorbCoefficient>().GetValue();
            float tisa = calculator.UpdatedParameter<HB_TensionInreasingStepsAmount>().GetValue();

            unroundValues.Clear();
            values.Clear();

            float unroundMaeu = eapr / (peuprc + (1 - cpd / ra) * (1 - ocac) / 2);
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
