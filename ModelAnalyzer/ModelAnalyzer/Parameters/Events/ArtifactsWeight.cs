namespace ModelAnalyzer.Parameters.Events
{
    class ArtifactsWeight : SingleParameter
    {
        public ArtifactsWeight()
        {
            type = ParameterType.In;
            title = "Вес артефактов";
            details = "Измеряется в средних добычах ТЗ";
            fractionalDigits = 2;
        }
    }
}
