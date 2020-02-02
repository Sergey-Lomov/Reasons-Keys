﻿namespace ModelAnalyzer.Parameters.Events
{
    class EventMaxMiningBonus : FloatSingleParameter
    {
        public EventMaxMiningBonus()
        {
            type = ParameterType.In;
            title = "Максимальный бонус добычи";
            details = "Имеется ввиду бонус на событиях";
            fractionalDigits = 0;
            tags.Add(ParameterTag.events);
            tags.Add(ParameterTag.mining);
        }
    }
}
