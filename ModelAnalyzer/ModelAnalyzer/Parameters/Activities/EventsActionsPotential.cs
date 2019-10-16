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

            float tp = RequestParmeter<TotalPotential>(calculator).GetValue();
            float ma = RequestParmeter<MotionAmount>(calculator).GetValue();
            float mp = RequestParmeter<MotionPrice>(calculator).GetValue();
            float eac = RequestParmeter<EventActionsCoef>(calculator).GetValue();

            value = unroundValue = (tp - ma * mp) * eac;

            return calculationReport;
        }
    }
}
