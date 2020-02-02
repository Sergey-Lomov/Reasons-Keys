namespace ModelAnalyzer.Parameters.Events
{
    class EventMinMiningBonus : FloatSingleParameter
    {
        public EventMinMiningBonus()
        {
            type = ParameterType.In;
            title = "Минимальный бонус добычи";
            details = "Имеется ввиду бонус на событиях";
            fractionalDigits = 0;
            tags.Add(ParameterTag.events);
            tags.Add(ParameterTag.mining);
        }
    }
}
