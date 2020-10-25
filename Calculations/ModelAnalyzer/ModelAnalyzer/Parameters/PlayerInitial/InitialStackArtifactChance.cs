namespace ModelAnalyzer.Parameters.PlayerInitial
{
    class InitialStackArtifactChance : FloatSingleParameter
    {
        public InitialStackArtifactChance()
        {
            type = ParameterType.In;
            title = "Шанс артефакта в изначальной раздаче";
            details = "Шанс вытащить хотябы одну карту с артефактом в изначальной раздаче";
            fractionalDigits = 3;
            tags.Add(ParameterTag.playerInitial);
            tags.Add(ParameterTag.artifacts);
        }
    }
}
