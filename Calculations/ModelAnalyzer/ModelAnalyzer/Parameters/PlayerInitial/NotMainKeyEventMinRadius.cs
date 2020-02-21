namespace ModelAnalyzer.Parameters.PlayerInitial
{
    class NotMainKeyEventMinRadius : FloatSingleParameter
    {
        public NotMainKeyEventMinRadius()
        {
            type = ParameterType.In;
            title = "Минимальный радиус не главного решающего события";
            details = "";
            fractionalDigits = 0;
            tags.Add(ParameterTag.playerInitial);
        }
    }
}
