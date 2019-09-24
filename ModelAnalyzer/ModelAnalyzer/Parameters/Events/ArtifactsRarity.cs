namespace ModelAnalyzer.Parameters.Events
{
    class ArtifactsRarity : SingleParameter
    {
        public ArtifactsRarity()
        {
            type = ParameterType.In;
            title = "Редкость артефактов";
            details = "Задает отношение кол-ва событий с артефактами к общему кол-ву событий континуума";
            fractionalDigits = 2;
            tags.Add(ParameterTag.events);
            tags.Add(ParameterTag.artifacts);
        }
    }
}
