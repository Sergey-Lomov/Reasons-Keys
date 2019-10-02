using System.Linq;

using ModelAnalyzer.Services;
using ModelAnalyzer.Parameters.Timing;

namespace ModelAnalyzer.Parameters.Topology
{
    class AveragePhasesDistance : FloatArrayParameter
    {
        private readonly float validFieldRadius = 4;
        private readonly string invalidMessageFormat = "Параметр был расчитан при \"{0}\" = {1}. Сейчас его значение не актуально.";

        public AveragePhasesDistance()
        {
            type = ParameterType.Inner;
            title = "Средние расстояния в фазах";
            details = "В разных фазах поле имеет разную конфигурацию, из-за чего математических формул для расчета вывести не удалось. Среднее расстояние определяется с помощью отдельной программы FieldAnalyser, перебирающей все пары узлов. Это значение зависимо от радиуса (4) поля и вытекающего из него кол-ва фаз.";
            fractionalDigits = 2;
            tags.Add(ParameterTag.topology);
        }

        internal override ParameterValidationReport Validate(Validator validator, Storage storage)
        {    
            var report = base.Validate(validator, storage);
            var size = storage.Parameter(typeof(PhasesAmount));
            ValidateSize(size, report);

            var radiusType = typeof(FieldRadius);
            float r = storage.SingleValue(radiusType);

            if (r != validFieldRadius)
            {
                var title = storage.Parameter(radiusType).title;
                var issue = string.Format(invalidMessageFormat, title, validFieldRadius);
                report.issues.Add(issue);
            }

            return report;
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            unroundValues.Clear();

            float pa = calculator.UpdatedSingleValue(typeof(PhasesAmount));
            var routesMap = calculator.UpdatedParameter<RoutesMap>();

            for (int i = 0; i < pa; i++)
            {
                var distances = routesMap.phasesRoutes[i].Select(r => r.distance);
                float average = distances.Count() > 0 ? (float)distances.Average() : 0;
                unroundValues.Add(average);
            }

            values = unroundValues;

            return calculationReport;
        }
    }
}
