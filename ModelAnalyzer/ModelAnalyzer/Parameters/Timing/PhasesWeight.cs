namespace ModelAnalyzer.Parameters.Timing
{
    class PhasesWeight : ArrayParameter
    {
        private readonly string arraySizeMessage = "Размер массива должен быть равен \"{0}\": {1}.";

        public PhasesWeight()
        {
            type = ParameterType.In;
            title = "Веса фаз";
            details = "Веса определяют то, сколько раундов будут длится фазы. Значения выбраны основываясь на ожидаемых ролях фаз.";
            fractionalDigits = 0;
        }

        internal override ParameterValidationReport Validate(Validator validator, Storage storage)
        {
            var report = base.Validate(validator, storage);
            var phasesAmountType = typeof(PhasesAmount);
            float pa = storage.GetSingleValue(phasesAmountType);

            if (pa != values.Count)
            {
                var title = storage.GetParameter(phasesAmountType).title;
                var issue = string.Format(arraySizeMessage, title, pa);
                report.issues.Add(issue);
            }

            return report;
        }
    }
}
