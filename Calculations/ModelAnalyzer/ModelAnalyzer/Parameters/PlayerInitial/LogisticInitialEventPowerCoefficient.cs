namespace ModelAnalyzer.Parameters.PlayerInitial
{
    class LogisticInitialEventPowerCoefficient : FloatSingleParameter
    {
        public LogisticInitialEventPowerCoefficient()
        {
            type = ParameterType.In;
            title = "ЛИС: коэф. силы";
            details = "Этот парамтр отражает то, насколько связи вперед логистического события мощнее, чем воздействие, которое можно оказать за ТЗ, потраченные на его организацию";
            fractionalDigits = 2;
            tags.Add(ParameterTag.playerInitial);
        }
    }
}
