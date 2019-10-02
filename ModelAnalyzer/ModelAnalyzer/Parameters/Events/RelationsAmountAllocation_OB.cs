using ModelAnalyzer.Services;

namespace ModelAnalyzer.Parameters.Events
{
    class RelationsAmountAllocation_OB : FloatArrayParameter
    {
        public RelationsAmountAllocation_OB()
        {
            type = ParameterType.In;
            title = "Распределение кол-ва связей (только назад)";
            details = "Указывает отношение карт с различным кол-вом связей в колоде. Но только среди карт не имеющих связей вперед.";
            fractionalDigits = 0;
            tags.Add(ParameterTag.events);
        }

        internal override ParameterValidationReport Validate(Validator validator, Storage storage)
        {
            var report = base.Validate(validator, storage);
            var mbr = storage.SingleValue(typeof(MinBackRelations));
            var emr = storage.SingleValue(typeof(EventMaxRelations));

            var validSize = emr - mbr + 1;
            ValidateSize(validSize, report);

            return report;
        }
    }
}
