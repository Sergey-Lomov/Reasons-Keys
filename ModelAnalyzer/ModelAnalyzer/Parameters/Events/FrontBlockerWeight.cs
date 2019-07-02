namespace ModelAnalyzer.Parameters.Events
{
    class FrontBlockerWeight : SingleParameter
    {
        public FrontBlockerWeight()
        {
            type = ParameterType.In;
            title = "Вес блокираторов вперед";
            details = "Используется для блокираторов направленных вперед. При расчете веса карты, вес связи умножается на среднюю стабильность, переносимую связью.";
            fractionalDigits = 2;
        }
    }
}
