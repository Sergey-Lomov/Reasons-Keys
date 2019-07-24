using ModelAnalyzer.Parameters.Events;
using ModelAnalyzer.Parameters.Mining;

namespace ModelAnalyzer.Parameters.Activities
{
    class EventImpactPrice : SingleParameter
    {
        public EventImpactPrice()
        {
            type = ParameterType.Inner;
            title = "Полная стоимость воздействия на событие";
            details = "";
            fractionalDigits = 2;
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float asi = calculator.UpdatedSingleValue(typeof(AverageStabilityIncrement));
            float am = calculator.UpdatedSingleValue(typeof(AverageMining));
            float eap = calculator.UpdatedSingleValue(typeof(EventsActionsPotential));
            float eca = calculator.UpdatedSingleValue(typeof(EventCreationAmount));
            float eia = calculator.UpdatedSingleValue(typeof(EventImpactAmount));
            float dc = calculator.UpdatedSingleValue(typeof(DestructionCoef));

            // See Mechanic doc for clarify formula
            value = unroundValue = eap / (eca * asi / dc + eia);

            return calculationReport;
        }
    }
}
