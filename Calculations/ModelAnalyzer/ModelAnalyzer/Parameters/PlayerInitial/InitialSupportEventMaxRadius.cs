namespace ModelAnalyzer.Parameters.PlayerInitial
{
    class InitialSupportEventMaxRadius : FloatSingleParameter
    {
        public InitialSupportEventMaxRadius()
        {
            type = ParameterType.In;
            title = "Максимальный радиус поддерживающей изначальной карты";
            details = "";
            fractionalDigits = 0;
            tags.Add(ParameterTag.playerInitial);
        }
    }
}
