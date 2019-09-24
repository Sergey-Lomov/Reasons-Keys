namespace ModelAnalyzer.Parameters.Mining
{
    class MiningAUCoef : SingleParameter
    {
        public MiningAUCoef()
        {
            type = ParameterType.In;
            title = "Отношение добычи к общему кол-ву ЕА";
            details = "Это отношеие определяет то, как много игрового времени (ЕА) игрок должен посвящать добыче ТЗ. Это параметр должен иметь не слишком маленькое значение, чтобы у игроков была возможность получить преимущество, добывая ТЗ более эффекитвно чем со средней добычей.";
            fractionalDigits = 2;
            tags.Add(ParameterTag.mining);
            tags.Add(ParameterTag.activities);
        }
    }
}
