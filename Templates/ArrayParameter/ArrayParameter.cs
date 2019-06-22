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

        internal override void Calculate(Calculator calculator)
        {
             float p = calculator.GetSingleValue(typeof(ParamName));
        }
    }
}
