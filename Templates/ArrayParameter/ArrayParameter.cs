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
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float p = calculator.GetSingleValue(typeof(ParamName));

            return calculationReport;
        }

        internal override ParameterValidationReport Validate(Validator validator, Storage storage)
        {
            var size = storage.GetParameter(typeof(SizeParamName));
            var report = Validate(validator, storage, size);
            return report;
        }
    }
}
