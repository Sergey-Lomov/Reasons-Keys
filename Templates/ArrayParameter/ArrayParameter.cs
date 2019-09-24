namespace $rootnamespace$
{
    class $safeitemname$ : ArrayParameter
    {
        public $safeitemname$()
        {
            type = ParameterType.;
            title = "";
            details = "";
            fractionalDigits = 0;
            tags.Add(ParameterTag.);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float p = calculator.UpdateSingleValue(typeof(ParamName));

            unroundValues.Clear();
            values.Clear();

            return calculationReport;
        }

        internal override ParameterValidationReport Validate(Validator validator, Storage storage)
        {
            var report = base.Validate(validator, storage);
            var size = storage.Parameter(typeof(SizeParamName));
            ValidateSize(size, report);
            return report;
        }
    }
}
