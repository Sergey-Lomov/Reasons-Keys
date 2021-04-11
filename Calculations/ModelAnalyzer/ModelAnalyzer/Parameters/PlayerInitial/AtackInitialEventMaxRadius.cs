namespace ModelAnalyzer.Parameters.PlayerInitial
{
    class AtackInitialEventMaxRadius : FloatSingleParameter
    {
        public AtackInitialEventMaxRadius()
        {
            type = ParameterType.In;
            title = "АИС: максимальный радиус";
            details = "Максимальный радиус атакующей изначальной карты";
            fractionalDigits = 0;
            tags.Add(ParameterTag.playerInitial);
        }
    }
}
