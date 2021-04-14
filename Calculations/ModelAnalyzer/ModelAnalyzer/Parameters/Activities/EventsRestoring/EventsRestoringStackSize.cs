using System.Linq;
using ModelAnalyzer.Services;
using ModelAnalyzer.Parameters.General;

namespace ModelAnalyzer.Parameters.Activities.EventsRestoring
{
    class EventRestoringStackSize : FloatArrayParameter
    {
        public EventRestoringStackSize()
        {
            type = ParameterType.Out;
            title = "Кол-во событий в раздаче";
            details = "Кол-во событий, из которых игрок выбирает себе новое событие после организации одного из тех, которые были у него в руке. Зависит от кол-ва игроков.";
            fractionalDigits = 0;
            tags.Add(ParameterTag.activities);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            var module = RequestModule<EventsRestoringModule>(calculator);

            if (!calculationReport.IsSuccess)
                return calculationReport;

            ClearValues();
            values = unroundValues = module.stacks.Select(s => (float)s.size).ToList();

            return calculationReport;
        }

        internal override ParameterValidationReport Validate(Validator validator, Storage storage)
        {
            var report = base.Validate(validator, storage);
            var minpa = (int)storage.Parameter<MinPlayersAmount>().GetValue();
            var maxpa = (int)storage.Parameter<MaxPlayersAmount>().GetValue();

            ValidateSize(maxpa - minpa + 1, report);
            return report;
        }
    }
}
