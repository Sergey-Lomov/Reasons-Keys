namespace ModelAnalyzer.Parameters.Events.Weight
{
    class ArtifactsWeight : FloatSingleParameter
    {
        public ArtifactsWeight()
        {
            type = ParameterType.In;
            title = "Вес артефактов";
            details = "Измеряется в средних добычах ТЗ";
            fractionalDigits = 2;
            tags.Add(ParameterTag.events);
            tags.Add(ParameterTag.eventsWeight);
        }
    }
}
