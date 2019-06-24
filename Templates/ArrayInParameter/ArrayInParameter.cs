namespace $rootnamespace$
{
    class $safeitemname$ : ArrayParameter
    {
        public $safeitemname$()
        {
            type = ParameterType.In;
            title = "";
            details = "";
            fractionalDigits = 0;
        }

        internal override ParameterValidationReport Validate(Validator validator, Storage storage)
        {
            var size = storage.Parameter(typeof(SizeParamName));
            var report = Validate(validator, storage, size);
            return report;
        }
    }
}
