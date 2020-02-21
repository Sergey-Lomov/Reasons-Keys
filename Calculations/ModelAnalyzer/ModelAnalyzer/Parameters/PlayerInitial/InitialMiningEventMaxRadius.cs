namespace ModelAnalyzer.Parameters.PlayerInitial
{
    class InitialMiningEventMaxRadius : FloatSingleParameter
    {
        public InitialMiningEventMaxRadius()
        {
            type = ParameterType.In;
            title = "Максимальный радиус добывающей изначальной карты";
            details = "";
            fractionalDigits = 0;
            tags.Add(ParameterTag.playerInitial);
        }
    }
}
