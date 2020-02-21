namespace ModelAnalyzer.Parameters.PlayerInitial
{
    class InitialAtackEventMaxRadius : FloatSingleParameter
    {
        public InitialAtackEventMaxRadius()
        {
            type = ParameterType.In;
            title = "Максимальный радиус атакующей изначальной карты";
            details = "";
            fractionalDigits = 0;
            tags.Add(ParameterTag.playerInitial);
        }
    }
}
