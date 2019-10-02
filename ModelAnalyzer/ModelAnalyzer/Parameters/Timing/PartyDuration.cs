using ModelAnalyzer.Services;
using ModelAnalyzer.Parameters.Activities;

namespace ModelAnalyzer.Parameters.Timing
{
    class PartyDuration : FloatSingleParameter
    {
        public PartyDuration()
        {
            type = ParameterType.Indicator;
            title = "Продолжительность партии (мин)";
            details = "Оценка продолжительонсти партии, в случае если игроки используют рекомендованный лимит времени на ход.";
            fractionalDigits = 0;
            tags.Add(ParameterTag.timing);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float md = calculator.UpdatedSingleValue(typeof(MoveDuration));
            float ra = calculator.UpdatedSingleValue(typeof(RoundAmount));
            float rd = calculator.UpdatedSingleValue(typeof(RealisationDuration));
            float ea = calculator.UpdatedSingleValue(typeof(EventCreationAmount));
            float pa = calculator.UpdatedSingleValue(typeof(MaxPlayersAmount));

            value = unroundValue = md * ra * pa  + rd * ea * pa;

            return calculationReport;
        }
    }
}
