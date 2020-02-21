namespace ModelAnalyzer.Parameters.PlayerInitial
{
    class MainKeyEventMinRadius : FloatSingleParameter
    {
        public MainKeyEventMinRadius()
        {
            type = ParameterType.In;
            title = "Минимальный радиус главного решающего события";
            details = "";
            fractionalDigits = 0;
            tags.Add(ParameterTag.playerInitial);
        }
    }
}
