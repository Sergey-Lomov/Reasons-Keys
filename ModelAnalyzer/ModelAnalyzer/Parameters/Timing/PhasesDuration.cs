using System;
using System.Linq;

namespace ModelAnalyzer.Parameters.Timing
{
    class PhasesDuration : ArrayParameter
    {
        private readonly string roundingIssue = "Невозможно корректно округлить значения. Сумма округленных значений отличется суммы не округленных.";

        public PhasesDuration()
        {
            type = ParameterType.Out;
            title = "Длительность фаз";
            details = "Длительность фаз основанная на их весе";
            fractionalDigits = 0;
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float ra = calculator.GetUpdateSingleValue(typeof(RoundAmount));
            float[] pw = calculator.GetUpdateArrayValue(typeof(PhasesWeight));

            unroundValues.Clear();
            values.Clear();

            foreach (float phaseWeight in pw)
            {
                float duration = phaseWeight / pw.Sum() * ra;
                unroundValues.Add(duration);

                float roundDuration = (float) Math.Round(duration);
                values.Add(roundDuration);
            }

            if (unroundValues.Sum() != values.Sum())
            {
                calculationReport.Failed(roundingIssue);
            }

            return calculationReport;
        }

        internal override ParameterValidationReport Validate(Validator validator, Storage storage)
        {
            var size = storage.GetParameter(typeof(PhasesAmount));
            var report = Validate(validator, storage, size);
            return report;
        }
    }
}
