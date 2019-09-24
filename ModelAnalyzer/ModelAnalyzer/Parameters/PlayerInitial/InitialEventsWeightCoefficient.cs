namespace ModelAnalyzer.Parameters.PlayerInitial
{
    class InitialEventsWeightCoefficient : SingleParameter
    {
        public InitialEventsWeightCoefficient()
        {
            type = ParameterType.In;
            title = "Коэф. веса изначальных событий";
            details = "Определяет отношение среднего веса изначальных карт кк среднему весу карт континуума. С его помощью можно управлять тем, насколько игроки будут заинтересованы в поиске карт среди карт континуума (в противовес использованию карт из изначальной раздачи).";
            fractionalDigits = 0;
            tags.Add(ParameterTag.playerInitial);
        }
    }
}
