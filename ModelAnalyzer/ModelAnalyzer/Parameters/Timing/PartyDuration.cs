using ModelAnalyzer.Services;
using ModelAnalyzer.Parameters.General;
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

            float md = RequestParmeter<MoveDuration>(calculator).GetValue();
            float ra = RequestParmeter<RoundAmount>(calculator).GetValue();
            float rd = RequestParmeter<RealisationDuration>(calculator).GetValue();
            float ea = RequestParmeter<EventCreationAmount>(calculator).GetValue();
            float pa = RequestParmeter<MaxPlayersAmount>(calculator).GetValue();

            value = unroundValue = md * ra * pa  + rd * ea * pa;

            return calculationReport;
        }
    }
}
