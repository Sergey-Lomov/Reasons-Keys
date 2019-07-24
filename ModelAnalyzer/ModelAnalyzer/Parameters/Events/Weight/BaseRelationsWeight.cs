namespace ModelAnalyzer.Parameters.Events.Weight
{
    class BaseRelationsWeight : SingleParameter
    {
        public BaseRelationsWeight()
        {
            type = ParameterType.In;
            title = "Вес базовых связей";
            details = "Используется для блокираторов назад, сопряженных причин назад и одной из несопряженных причин назад. При расчете веса карты, вес связи умножается на среднюю стабильность, переносимую связью.";
            fractionalDigits = 2;
        }
    }
}
