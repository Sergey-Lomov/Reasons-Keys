namespace ModelAnalyzer.Parameters.PlayerInitial
{
    class MiningInitialCardCoefficient : SingleParameter
    {
        public MiningInitialCardCoefficient()
        {
            type = ParameterType.In;
            title = "Коэф. добывающей изначальной карты";
            details = "Определяет бонус добычи для добывающей изначальной карты. Подробнее см. \"Изначальные события\" в разделе \"Начальное состояние игроков\" основного документа по механике";
            fractionalDigits = 2;
        }
    }
}
