using ModelAnalyzer.Services;

namespace ModelAnalyzer.Parameters.Activities.EventsRestoring
{
    class EstimatedUnluckyStackChance : FloatSingleParameter
    {
        private const string normalisationIssue = "Значение параметра должно быть больше 0 и меньше 1";

        public EstimatedUnluckyStackChance()
        {
            type = ParameterType.In;
            title = "Ожидаемый шанс неблагоприятной раздачи событий";
            details = "Входящий параметр, определяющий ожидаемый шанс того, что при доборе карт (после организации события) игрок получит на выбор только карты с -1 очком его ветви либо карты с шаблоном +1/+1 с ветвями противников. Так как кол-во карт в раздаче может быть только целым, реальный шанс будет отличатся от ожидаемого. Этот параметр должен быть задан в границах от 0 до 1.";
            fractionalDigits = 2;
            tags.Add(ParameterTag.activities);
        }

        internal override ParameterValidationReport Validate(Validator validator, Storage storage)
        {
            var report = base.Validate(validator, storage);
            if (value == 0 || value >= 1)
            {
                report.AddIssue(normalisationIssue);
            }

            return report;
        }
    }
}
