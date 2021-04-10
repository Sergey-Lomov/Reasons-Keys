namespace ModelAnalyzer.Parameters.PlayerInitial
{
    class LogisticInitialEventMaxRadius : FloatSingleParameter
    {
        public LogisticInitialEventMaxRadius()
        {
            type = ParameterType.In;
            title = "ЛИС: максимальный радиус";
            details = "";
            fractionalDigits = 0;
            tags.Add(ParameterTag.playerInitial);
        }
    }
}
