using ModelAnalyzer.Parameters.Activities;

namespace ModelAnalyzer.Parameters.Timing
{
    class PartyDuration : SingleParameter
    {
        public PartyDuration()
        {
            type = ParameterType.Indicator;
            title = "Продолжительность партии (мин)";
            details = "Оценка продолжительонсти партии, в случае если игроки используют рекомендованный лимит времени на ход.";
            fractionalDigits = 0;
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float md = calculator.GetUpdateSingleValue(typeof(MoveDuration));
            float ra = calculator.GetUpdateSingleValue(typeof(RoundAmount));
            float rd = calculator.GetUpdateSingleValue(typeof(RealisationDuration));
            float ea = calculator.GetUpdateSingleValue(typeof(EventCreationAmount));
            float pa = calculator.GetUpdateSingleValue(typeof(MaxPlayersAmount));

            value = unroundValue = md * ra * pa  + rd * ea * pa;

            return calculationReport;
        }
    }
}
