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
            var size = storage.Parameter(typeof(PhasesAmount));
            var report = Validate(validator, storage, size);
            return report;
        }
    }
}
