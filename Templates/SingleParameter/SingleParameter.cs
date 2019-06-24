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

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float p = calculator.UpdateSingleValue(typeof(ParamName));

            value = ;

            return calculationReport;
        }
    }
}
