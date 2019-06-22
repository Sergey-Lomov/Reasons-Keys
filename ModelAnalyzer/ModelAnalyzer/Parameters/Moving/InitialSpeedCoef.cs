namespace ModelAnalyzer.Parameters.Moving
{
    class InitialSpeedCoef : SingleParameter
    {
        public InitialSpeedCoef()
        {
            type = ParameterType.In;
            title = "Коэф. начальной скорости";
            details = "Задает отношение начальной скорости сфер к среднему расстоянию";
            fractionalDigits = 2;
        }
    }
}
