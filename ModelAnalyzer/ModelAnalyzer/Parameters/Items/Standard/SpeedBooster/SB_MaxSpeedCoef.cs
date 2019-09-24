namespace ModelAnalyzer.Parameters.Items.Standard.SpeedBooster
{
    class SB_MaxSpeedCoef : SingleParameter
    {
        public SB_MaxSpeedCoef()
        {
            type = ParameterType.In;
            title = "Коэф. максимальной скорости";
            details = "Задает отношение максимальной скорости (с использование всех базовых ускорителей) к среднему расстоянию";
            fractionalDigits = 2;
            tags.Add(ParameterTag.items);
            tags.Add(ParameterTag.moving);
        }
    }
}
