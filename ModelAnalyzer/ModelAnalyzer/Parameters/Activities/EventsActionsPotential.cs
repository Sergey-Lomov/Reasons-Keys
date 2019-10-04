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

            float tp = calculator.UpdatedParameter<TotalPotential>().GetValue();
            float ma = calculator.UpdatedParameter<MotionAmount>().GetValue();
            float mp = calculator.UpdatedParameter<MotionPrice>().GetValue();
            float eac = calculator.UpdatedParameter<EventActionsCoef>().GetValue();

            value = unroundValue = (tp - ma * mp) * eac;

            return calculationReport;
        }
    }
}
