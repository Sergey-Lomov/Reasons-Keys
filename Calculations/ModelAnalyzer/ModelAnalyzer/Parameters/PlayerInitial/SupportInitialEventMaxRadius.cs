namespace ModelAnalyzer.Parameters.PlayerInitial
{
    class SupportInitialEventMaxRadius : FloatSingleParameter
    {
        public SupportInitialEventMaxRadius()
        {
            type = ParameterType.In;
            title = "Максимальный радиус поддерживающей изначальной карты";
            details = "";
            fractionalDigits = 0;
            tags.Add(ParameterTag.playerInitial);
        }
    }
}
