using ModelAnalyzer.Services;

namespace ModelAnalyzer.Parameters.Events
{
    class MultyblockCardsAllocation_OB : FloatArrayParameter
    {
        public MultyblockCardsAllocation_OB()
        {
            type = ParameterType.In;
            title = "Распределение кол-ва блокираторов (только назад)";
            details = "Указывает отношение между картами с различным кол-вом блокираторов";
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
