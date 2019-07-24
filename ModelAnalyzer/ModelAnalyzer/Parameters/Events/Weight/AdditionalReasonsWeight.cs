namespace ModelAnalyzer.Parameters.Events.Weight
{
    class AdditionalReasonsWeight : SingleParameter
    {
        public AdditionalReasonsWeight()
        {
            type = ParameterType.In;
            title = "Вес дополнительных причин";
            details = "Используется для несопряженных причин назад (кроме одной, для которой используется базовый вес). При расчете веса карты, вес связи умножается на среднюю стабильность, переносимую связью.";
            fractionalDigits = 2;
        }
    }
}
