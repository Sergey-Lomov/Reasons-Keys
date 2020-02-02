using ModelAnalyzer.Services;

namespace $rootnamespace$
{
    class $safeitemname$ : FloatSingleParameter
    {
        public $safeitemname$()
        {
            type = ParameterType.;
            title = "";
            details = "";
            fractionalDigits = 2;
            tags.Add(ParameterTag.);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float p = RequestParmeter<ParamName>(calculator).GetValue();

            if (!calculationReport.IsSuccess)
                return calculationReport;

            value = ;

            return calculationReport;
        }
    }
}
