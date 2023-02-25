using ModelAnalyzer.Services;
using ModelAnalyzer.Parameters.Moving;

namespace ModelAnalyzer.Parameters.Activities
{
    class EventsActionsPotential : FloatSingleParameter
    {
        public EventsActionsPotential()
        {
            type = ParameterType.Inner;
            title = "Потенциал событийных действий.";
            details = "";
            fractionalDigits = 2;
            tags.Add(ParameterTag.activities);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float tp = RequestParameter<TotalPotential>(calculator).GetValue();
            float ma = RequestParameter<MotionAmount>(calculator).GetValue();
            float mp = RequestParameter<MotionPrice>(calculator).GetValue();

            value = unroundValue = tp - ma * mp;

            return calculationReport;
        }
    }
}
