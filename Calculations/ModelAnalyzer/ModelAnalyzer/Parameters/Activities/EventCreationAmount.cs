using ModelAnalyzer.Services;

namespace ModelAnalyzer.Parameters.Activities
{
    class EventCreationAmount : FloatSingleParameter
    {
        public EventCreationAmount()
        {
            type = ParameterType.Inner;
            title = "Стандартное кол-во организации событий";
            details = "Предполагается, что в течении партии игрок будет организовывать в среднем указанное кол-во событий.";
            fractionalDigits = 2;
            tags.Add(ParameterTag.activities);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float keca = RequestParameter<KeyEventCreationAmount>(calculator).GetValue();
            float nkeca = RequestParameter<NokeyEventCreationAmount>(calculator).GetValue();

            if (!calculationReport.IsSuccess)
                return calculationReport;

            value = unroundValue = keca + nkeca;

            return calculationReport;
        }
    }
}
