namespace $rootnamespace$
{
    class $safeitemname$ : SingleParameter
    {
        public $safeitemname$()
        {
            type = ParameterType.;
            title = "";
            details = "";
            fractionalDigits = 2;
        }

        internal override void Calculate(Calculator calculator)
        {
            float p = calculator.GetSingleValue(typeof(ParamName));
        }
    }
}
