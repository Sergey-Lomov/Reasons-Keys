namespace ModelAnalyzer.Parameters.Events
{
    class EventMinMiningBonus : SingleParameter
    {
        public EventMinMiningBonus()
        {
            type = ParameterType.In;
            title = "Минимальный бонус добычи";
            details = "Имеется ввиду бонус на событиях";
            fractionalDigits = 0;
        }
    }
}
