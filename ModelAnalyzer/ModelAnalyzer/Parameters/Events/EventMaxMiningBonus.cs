namespace ModelAnalyzer.Parameters.Events
{
    class EventMaxMiningBonus : SingleParameter
    {
        public EventMaxMiningBonus()
        {
            type = ParameterType.In;
            title = "Максимальный бонус добычи";
            details = "Имеется ввиду бонус на событиях";
            fractionalDigits = 0;
        }
    }
}
