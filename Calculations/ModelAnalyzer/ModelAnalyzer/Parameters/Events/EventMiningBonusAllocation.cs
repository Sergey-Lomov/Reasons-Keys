using ModelAnalyzer.Services;

namespace ModelAnalyzer.Parameters.Events
{
    class EventMiningBonusAllocation : FloatArrayParameter
    {
        public EventMiningBonusAllocation()
        {
            type = ParameterType.In;
            title = "Распределение бонусов ТЗ";
            details = "Задает пропорции в которых различные бонусы добычи должны встречаться на событиях континуума.";
            fractionalDigits = 0;
            tags.Add(ParameterTag.events);
            tags.Add(ParameterTag.mining);
        }

        internal override ParameterValidationReport Validate(Validator validator, Storage storage)
        {
            var report = base.Validate(validator, storage);
            var max = storage.Parameter<EventMaxMiningBonus>().GetValue();
            var min = storage.Parameter<EventMinMiningBonus>().GetValue();

            var validSize = max - min + 1;
            ValidateSize(validSize, report);

            return report;
        }
    }
}
