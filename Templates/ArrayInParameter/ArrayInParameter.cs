namespace $rootnamespace$
{
    class $safeitemname$ : FloatArrayParameter
    {
        public $safeitemname$()
        {
            type = ParameterType.In;
            title = "";
            details = "";
            fractionalDigits = 0;
            tags.Add(ParameterTag.);
        }

        internal override ParameterValidationReport Validate(Validator validator, Storage storage)
        {
            var report = base.Validate(validator, storage);
            var size = storage.Parameter<SizeParamName>().GetValue();
            ValidateSize(size, report);
            return report;
}
    }
}
