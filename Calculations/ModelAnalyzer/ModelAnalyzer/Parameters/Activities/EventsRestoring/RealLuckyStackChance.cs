using ModelAnalyzer.Parameters.General;
using ModelAnalyzer.Services;
using System.Linq;

namespace ModelAnalyzer.Parameters.Activities.EventsRestoring
{
    class RealLuckyStackChance : FloatArrayParameter
    {
        public RealLuckyStackChance()
        {
            type = ParameterType.Indicator;
            title = "Реальный шанс благоприятной раздачи";
            details = "Реальный шанс того, что добирая карты после организации события из руки, игрок получит возможность выбрать события с очками ветви -1/-1 для ветвей проитвников или +1 для своей ветви.";
            fractionalDigits = 2;
            tags.Add(ParameterTag.activities);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            var module = RequestModule<EventsRestoringModule>(calculator);

            if (!calculationReport.IsSuccess)
                return calculationReport;

            ClearValues();
            values = unroundValues = module.stacks.Select(s => s.luckyChance).ToList();

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
