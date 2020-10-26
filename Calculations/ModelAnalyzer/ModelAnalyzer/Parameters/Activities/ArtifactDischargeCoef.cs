namespace ModelAnalyzer.Parameters.Activities
{
    class ArtifactDischargeCoef : FloatSingleParameter
    {
        public ArtifactDischargeCoef()
        {
            type = ParameterType.In;
            title = "Коэф. разрядки артефактов";
            details = "Множитель, применяемый к стоимости артефакта в виде ТЗ для получения компенсации при разрядке артефактов";
            fractionalDigits = 2;
            tags.Add(ParameterTag.activities);
            tags.Add(ParameterTag.artifacts);
        }
    }
}
