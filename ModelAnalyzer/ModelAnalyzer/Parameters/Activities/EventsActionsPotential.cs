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
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float tp = calculator.UpdateSingleValue(typeof(TotalPotential));
            float ma = calculator.UpdateSingleValue(typeof(MotionAmount));
            float mp = calculator.UpdateSingleValue(typeof(MotionPrice));
            float eac = calculator.UpdateSingleValue(typeof(EventActionsCoef));

            value = unroundValue = (tp - ma * mp) * eac;

            return calculationReport;
        }
    }
}
