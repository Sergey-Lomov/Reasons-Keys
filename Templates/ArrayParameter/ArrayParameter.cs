namespace $rootnamespace$
{
    class $safeitemname$ : FloatArrayParameter
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

            float p = calculator.UpdatedParameter<ParamName>.GetValue();

            unroundValues.Clear();
            values.Clear();

            return calculationReport;
        }

        internal override ParameterValidationReport Validate(Validator validator, Storage storage)
        {
            var report = base.Validate(validator, storage);
            var size = storage.Parameter<SizeParamName>.GetValue();
            ValidateSize(size, report);
            return report;
        }
    }
}
