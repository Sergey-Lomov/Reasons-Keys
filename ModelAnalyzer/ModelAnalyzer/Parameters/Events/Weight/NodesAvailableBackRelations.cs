using ModelAnalyzer.Services;

namespace ModelAnalyzer.Parameters.Events.Weight
{
    class NodesAvailableBackRelations : FloatArrayParameter
    {
        const string invalidSizeMessage = "Размер массива должен быть от 7 (от 0 до 6)";
        const int validSize = 7;

        public NodesAvailableBackRelations()
        {
            type = ParameterType.In;
            title = "Распределение возможных связей назад";
            details = "Кол-во узлов в которых возможно различное кол-во связей назад. В массиве должно быть 7 значений: кол-во узлов у которых возможно 0 связей назад, 1 связь, 2 и т.д. до 6.";
            fractionalDigits = 0;
        }

        internal override ParameterValidationReport Validate(Validator validator, Storage storage)
        {
            var report = base.Validate(validator, storage);
            ValidateSize(validSize, invalidSizeMessage, report);
            return report;
        }
    }
}
