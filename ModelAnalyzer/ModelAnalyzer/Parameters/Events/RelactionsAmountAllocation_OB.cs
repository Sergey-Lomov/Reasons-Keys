namespace ModelAnalyzer.Parameters.Events
{
    class RelactionsAmountAllocation_OB : ArrayParameter
    {
        public RelactionsAmountAllocation_OB()
        {
            type = ParameterType.In;
            title = "Распределение кол-ва связей (только назад)";
            details = "Указывает отношение карт с различным кол-вом связей в колоде. Но только среди карт не имеющих связей вперед.";
            fractionalDigits = 0;
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
