namespace ModelAnalyzer.Parameters.Events.Weight
{
    class FrontReasonsWeight : FloatSingleParameter
    {
        public FrontReasonsWeight()
        {
            type = ParameterType.In;
            title = "Вес причин вперед";
            details = "Используется для причин направленных вперед. При расчете веса карты, вес связи умножается на среднюю стабильность, переносимую связью.";
            fractionalDigits = 2;
            tags.Add(ParameterTag.events);
            tags.Add(ParameterTag.eventsWeight);
        }
    }
}
