namespace ModelAnalyzer.Parameters.Events
{
    class FrontEventsCoef : FloatSingleParameter
    {
        public FrontEventsCoef()
        {
            type = ParameterType.In;
            title = "Коэф. событий со связями вперед";
            details = "Определяет как много карт континуума должны иметь связи вперед";
            fractionalDigits = 2;
            tags.Add(ParameterTag.events);
        }
    }
}
