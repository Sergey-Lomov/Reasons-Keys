using ModelAnalyzer.Services;

namespace ModelAnalyzer.Parameters.Events
{
    class RelationsAmountAllocation_2D : FloatArrayParameter
    {
        public RelationsAmountAllocation_2D()
        {
            type = ParameterType.In;
            title = "Распределение кол-ва связей (2 стороны)";
            details = "Указывает отношение карт с различным кол-вом связей в колоде. Но только среди карт имеющих связи вперед. Длина этого массива всегда должна быть на 1 меньше длины массива для карт(только назад). Так как они должны иметь еще как минимум одну связь вперед и следовательно не могут иметь минимальное кол-во связей.";
            fractionalDigits = 0;
            tags.Add(ParameterTag.events);
        }

        internal override ParameterValidationReport Validate(Validator validator, Storage storage)
        {
            var report = base.Validate(validator, storage);
            var mbr = storage.SingleValue(typeof(MinBackRelations));
            var emr = storage.SingleValue(typeof(EventMaxRelations));

            var validSize = emr - mbr;
            ValidateSize(validSize, report);

            return report;
        }
    }
}
