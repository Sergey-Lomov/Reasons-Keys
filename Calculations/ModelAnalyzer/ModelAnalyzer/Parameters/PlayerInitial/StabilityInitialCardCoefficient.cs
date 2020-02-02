namespace ModelAnalyzer.Parameters.PlayerInitial
{
    class StabilityInitialCardCoefficient : FloatSingleParameter
    {
        public StabilityInitialCardCoefficient()
        {
            type = ParameterType.In;
            title = "Коэф. стабилизирующей изначальной карты";
            details = "Определяет прирост табильности для стабилизирующей изначальной карты. Подробнее см. \"Изначальные события\" в разделе \"Начальное состояние игроков\" основного документа по механике";
            fractionalDigits = 2;
            tags.Add(ParameterTag.playerInitial);
        }
    }
}
