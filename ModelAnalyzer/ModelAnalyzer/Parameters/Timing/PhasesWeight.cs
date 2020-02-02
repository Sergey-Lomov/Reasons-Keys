﻿using ModelAnalyzer.Services;

namespace ModelAnalyzer.Parameters.Timing
{
    class PhasesWeight : FloatArrayParameter
    {
        public PhasesWeight()
        {
            type = ParameterType.In;
            title = "Веса фаз";
            details = "Веса определяют то, сколько раундов будут длится фазы. Значения выбраны основываясь на ожидаемых ролях фаз.";
            fractionalDigits = 0;
            tags.Add(ParameterTag.timing);
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
