using ModelAnalyzer.Parameters.Timing;

namespace ModelAnalyzer.Parameters.Moving
{
    class AveragePhasesDistance : ArrayParameter
    {
        private readonly float validFieldRadius = 4;
        private readonly string invalidMessageFormat = "Параметр был расчитан при \"{0}\" = {1}. Сейчас его значение не актуально.";

        public AveragePhasesDistance()
        {
            type = ParameterType.In;
            title = "Средние расстояния в фазах";
            details = "В разных фазах поле имеет разную конфигурацию, из-за чего математических формул для расчета вывести не удалось. Среднее расстояние определяется с помощью отдельной программы FieldAnalyser, перебирающей все пары узлов. Это значение зависимо от радиуса (4) поля и вытекающего из него кол-ва фаз.";
            fractionalDigits = 2;
        }

        internal override ParameterValidationReport Validate(Validator validator, Storage storage)
        {
            var size = storage.GetParameter(typeof(PhasesAmount));
            var report = Validate(validator, storage, size);

            var radiusType = typeof(FieldRadius);
            float r = storage.GetSingleValue(radiusType);

            if (r != validFieldRadius)
            {
                var title = storage.GetParameter(radiusType).title;
                var issue = string.Format(invalidMessageFormat, title, validFieldRadius);
                report.issues.Add(issue);
            }

            return report;
        }
    }
}
