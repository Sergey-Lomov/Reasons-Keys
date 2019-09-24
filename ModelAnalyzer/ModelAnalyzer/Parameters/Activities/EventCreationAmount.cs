namespace ModelAnalyzer.Parameters.Activities
{
    class EventCreationAmount : SingleParameter
    {
        public EventCreationAmount()
        {
            type = ParameterType.Inner;
            title = "Стандартное кол-во организации событий";
            details = "Предполагается, что в течении партии игрок будет организовывать в среднем указанное кол-во событий.";
            fractionalDigits = 1;
            tags.Add(ParameterTag.activities);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float keca = calculator.UpdatedSingleValue(typeof(KeyEventCreationAmount));
            float nkeca = calculator.UpdatedSingleValue(typeof(UnkeyEventCreationAmount));

            value = unroundValue = keca + nkeca;

            return calculationReport;
        }
    }
}
