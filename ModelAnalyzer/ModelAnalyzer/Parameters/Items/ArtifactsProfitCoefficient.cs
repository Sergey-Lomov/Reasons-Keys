namespace ModelAnalyzer.Parameters.Items
{
    class ArtifactsProfitCoefficient : SingleParameter
    {
        public ArtifactsProfitCoefficient()
        {
            type = ParameterType.In;
            title = "Коэф. выгодности артефактов";
            details = "Задает отношение выгодности артефактов к выгодности среднего предмета, не являющегося артифактом";
            fractionalDigits = 2;
            tags.Add(ParameterTag.items);
            tags.Add(ParameterTag.artifacts);
        }
    }
}
