namespace ModelAnalyzer.Parameters.PlayerInitial
{
    class AtackInitialEventMaxRadius : FloatSingleParameter
    {
        public AtackInitialEventMaxRadius()
        {
            type = ParameterType.In;
            title = "Максимальный радиус атакующей изначальной карты";
            details = "";
            fractionalDigits = 0;
            tags.Add(ParameterTag.playerInitial);
        }
    }
}
