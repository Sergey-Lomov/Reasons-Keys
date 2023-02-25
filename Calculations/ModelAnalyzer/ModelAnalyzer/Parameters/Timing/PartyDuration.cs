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

            float md = RequestParameter<MoveDuration>(calculator).GetValue();
            float ra = RequestParameter<RoundAmount>(calculator).GetValue();
            float rd = RequestParameter<RealisationDuration>(calculator).GetValue();
            float ea = RequestParameter<EventCreationAmount>(calculator).GetValue();
            float pa = RequestParameter<MaxPlayersAmount>(calculator).GetValue();

            if (!calculationReport.IsSuccess)
                return calculationReport;

            value = unroundValue = md * ra * pa  + rd * ea * pa;

            return calculationReport;
        }
    }
}
