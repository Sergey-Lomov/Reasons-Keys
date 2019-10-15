using ModelAnalyzer.Services;

namespace ModelAnalyzer.Parameters.Activities
{
    class MiningAmount : FloatSingleParameter
    {
        public MiningAmount()
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

            float p = calculator.UpdatedParameter<ParamName>().GetValue();

            value = ;

            return calculationReport;
        }
    }
}
