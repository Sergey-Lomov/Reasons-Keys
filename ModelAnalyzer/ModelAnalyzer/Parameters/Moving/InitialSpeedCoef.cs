namespace ModelAnalyzer.Parameters.Moving
{
    class InitialSpeedCoef : FloatSingleParameter
    {
        public InitialSpeedCoef()
        {
            type = ParameterType.In;
            title = "Коэф. начальной скорости";
            details = "Задает отношение начальной скорости сфер к среднему расстоянию";
            fractionalDigits = 2;
            tags.Add(ParameterTag.moving);
        }
    }
}
