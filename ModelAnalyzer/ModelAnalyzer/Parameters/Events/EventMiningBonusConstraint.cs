namespace ModelAnalyzer.Parameters.Events
{
    class EventMiningBonusConstraint : SingleParameter
    {
        public EventMiningBonusConstraint()
        {
            type = ParameterType.In;
            title = "Допустимый бонус ТЗ";
            details = "Здает отношение между добычей на радиусе без бонуса и максимально допустимой добычей с бонусом. На основе этого отношения к картам с большим бонусом добычи будет добавлено ограничение на использование только на определенных радиусах.";
            fractionalDigits = 2;
            tags.Add(ParameterTag.mining);
        }
    }
}
