namespace ModelAnalyzer.Parameters.Events
{
    class BrachPointsTemplatesAllocation : ArrayParameter
    {
        const int BrachPointsTeplatesAmount = 7;

        public BrachPointsTemplatesAllocation()
        {
            type = ParameterType.In;
            title = "Распределение шаблонов очков ветвей";
            details = "Шаблоном очков ветвей считается комбинация, такая как -1/0 или +1/+1. Распределение задается в порядке: -1/-1, +1/+1, -1/0, 0/-1, +1/0, 0/+1, 0/0";
            fractionalDigits = 0;
        }

        internal override ParameterValidationReport Validate(Validator validator, Storage storage)
        {
            var report = base.Validate(validator, storage);
            ValidateSize(BrachPointsTeplatesAmount, report);
            return report;
        }
    }
}
