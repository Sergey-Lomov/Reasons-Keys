namespace ModelAnalyzer.Parameters.PlayerInitial
{
    class SupportInitialEventMaxRadius : FloatSingleParameter
    {
        public SupportInitialEventMaxRadius()
        {
            type = ParameterType.In;
            title = "ПИС: Максимальный радиус";
            details = "Максимальный радиус поддерживающей изначальной карты";
            fractionalDigits = 0;
            tags.Add(ParameterTag.playerInitial);
        }
    }
}
