namespace ModelAnalyzer.Parameters.PlayerInitial
{
    class LogisticsInitialCardCoefficient : FloatSingleParameter
    {
        public LogisticsInitialCardCoefficient()
        {
            type = ParameterType.In;
            title = "Коэф. логистической изначальной карты";
            details = "Определяет вес связей для логистической изначальной карты. Подробнее см. \"Изначальные события\" в разделе \"Начальное состояние игроков\" основного документа по механике";
            fractionalDigits = 2;
            tags.Add(ParameterTag.playerInitial);
        }
    }
}
