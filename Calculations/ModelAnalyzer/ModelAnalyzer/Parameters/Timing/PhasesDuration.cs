using System;
using System.Linq;
using System.Collections.Generic;

using ModelAnalyzer.Services;

namespace ModelAnalyzer.Parameters.Timing
{
    class PhasesDuration : FloatArrayParameter
    {
        private readonly string roundingIssue = "Невозможно корректно округлить значения. Сумма округленных значений отличется суммы не округленных.";

        public PhasesDuration()
        {
            type = ParameterType.Out;
            title = "Длительность фаз";
            details = "Длительность фаз основанная на их весе";
            fractionalDigits = 0;
            tags.Add(ParameterTag.timing);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float ra = RequestParameter<RoundAmount>(calculator).GetValue();
            List<float> pw = RequestParameter<PhasesWeight>(calculator).GetValue();

            if (!calculationReport.IsSuccess)
                return calculationReport;

            unroundValues = new List<float>();
            values = new List<float>();

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
            var report = base.Validate(validator, storage);
            var size = storage.Parameter<PhasesAmount>();
            ValidateSize(size, report);
            return report;
        }
    }
}
