using ModelAnalyzer.Services;

namespace ModelAnalyzer.Parameters.Timing
{
    class AUPartyAmount : FloatSingleParameter
    {
        public AUPartyAmount()
        {
            type = ParameterType.Inner;
            title = "Кол-во ЕА на партию";
            details = "";
            fractionalDigits = 0;
            tags.Add(ParameterTag.timing);
            tags.Add(ParameterTag.activities);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float aum = RequestParmeter<AUMoveAmount>(calculator).GetValue();
            float ra = RequestParmeter<RoundAmount>(calculator).GetValue();

            value = unroundValue = ra * aum;

            return calculationReport;
        }
    }
}
