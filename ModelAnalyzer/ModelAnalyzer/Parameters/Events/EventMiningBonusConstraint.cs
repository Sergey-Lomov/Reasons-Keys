namespace ModelAnalyzer.Parameters.Events
{
    class EventMiningBonusConstraint : FloatSingleParameter
    {
        public EventMiningBonusConstraint()
        {
            type = ParameterType.In;
            title = "Допустимый бонус ТЗ";
            details = "Здает отношение между добычей на радиусе без бонуса и максимально допустимой добычей с бонусом. На основе этого отношения к картам с большим бонусом добычи будет добавлено ограничение на использование только на определенных радиусах.";
            fractionalDigits = 2;
            tags.Add(ParameterTag.events);
            tags.Add(ParameterTag.mining);
        }
    }
}
