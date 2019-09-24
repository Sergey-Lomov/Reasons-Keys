using ModelAnalyzer.Parameters.Events;
using ModelAnalyzer.Parameters.Mining;
using ModelAnalyzer.Parameters.Moving;

namespace ModelAnalyzer.Parameters.Activities
{
    class EventsActionsPotential : SingleParameter
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

            float tp = calculator.UpdatedSingleValue(typeof(TotalPotential));
            float ma = calculator.UpdatedSingleValue(typeof(MotionAmount));
            float mp = calculator.UpdatedSingleValue(typeof(MotionPrice));
            float eac = calculator.UpdatedSingleValue(typeof(EventActionsCoef));

            value = unroundValue = (tp - ma * mp) * eac;

            return calculationReport;
        }
    }
}
