namespace ModelAnalyzer.Parameters.PlayerInitial
{
    class LogisticInitialEventMaxRadius : FloatSingleParameter
    {
        public LogisticInitialEventMaxRadius()
        {
            type = ParameterType.In;
            title = "ЛИС: максимальный радиус";
            details = "Максимальный радиус логистической изначальной карты";
            fractionalDigits = 0;
            tags.Add(ParameterTag.playerInitial);
        }
    }
}
