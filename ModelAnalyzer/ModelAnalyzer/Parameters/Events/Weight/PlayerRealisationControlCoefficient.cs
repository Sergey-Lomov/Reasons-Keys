namespace ModelAnalyzer.Parameters.Events.Weight
{
    class PlayerRealisationControlCoefficient : FloatSingleParameter
    {
        public PlayerRealisationControlCoefficient()
        {
            type = ParameterType.In;
            title = "Коэф. контроля реализации игроком";
            details = "";
            fractionalDigits = 2;
        }
    }
}
