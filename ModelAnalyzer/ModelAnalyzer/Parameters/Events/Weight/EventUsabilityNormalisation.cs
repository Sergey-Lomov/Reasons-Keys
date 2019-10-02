namespace ModelAnalyzer.Parameters.Events.Weight
{
    class EventUsabilityNormalisation : FloatSingleParameter
    {
        public EventUsabilityNormalisation()
        {
            type = ParameterType.In;
            title = "Коэф. нормализации применимости событий";
            details = "Подробнее описан в основном документе по механике в разделе \"События континуума\", подраздел \"Оценка веса тайла\"";
            fractionalDigits = 2;
        }
    }
}
