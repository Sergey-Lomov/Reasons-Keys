namespace ModelAnalyzer.Parameters.Events.Weight
{
    class FrontBlockerWeight : FloatSingleParameter
    {
        public FrontBlockerWeight()
        {
            type = ParameterType.In;
            title = "Вес блокираторов вперед";
            details = "Используется для блокираторов направленных вперед. При расчете веса карты, вес связи умножается на среднюю стабильность, переносимую связью.";
            fractionalDigits = 2;
            tags.Add(ParameterTag.events);
            tags.Add(ParameterTag.eventsWeight);
        }
    }
}
