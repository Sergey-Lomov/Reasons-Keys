namespace ModelAnalyzer.Parameters.Moving
{
    class MaxSpeedCoef : SingleParameter
    {
        public MaxSpeedCoef()
        {
            type = ParameterType.In;
            title = "Коэф. максимальной скорости";
            details = "Задает отношение максимальной скорости (с использование всех базовых ускорителей) к среднему расстоянию";
            fractionalDigits = 2;
        }
    }
}
