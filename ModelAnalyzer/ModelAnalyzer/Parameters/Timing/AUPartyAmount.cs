namespace ModelAnalyzer.Parameters.Timing
{
    class AUPartyAmount : SingleParameter
    {
        public AUPartyAmount()
        {
            type = ParameterType.Inner;
            title = "Кол-во ЕА на партию";
            details = "";
            fractionalDigits = 0;
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float aum = calculator.UpdatedSingleValue(typeof(AUMoveAmount));
            float ra = calculator.UpdatedSingleValue(typeof(RoundAmount));

            value = unroundValue = ra * aum;

            return calculationReport;
        }
    }
}
