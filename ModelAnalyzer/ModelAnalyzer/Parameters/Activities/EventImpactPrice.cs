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

            float asi = calculator.UpdateSingleValue(typeof(AverageStabilityIncrement));
            float am = calculator.UpdateSingleValue(typeof(AverageMining));
            float eap = calculator.UpdateSingleValue(typeof(EventsActionsPotential));
            float eca = calculator.UpdateSingleValue(typeof(EventCreationAmount));
            float eia = calculator.UpdateSingleValue(typeof(EventImpactAmount));
            float dc = calculator.UpdateSingleValue(typeof(DestructionCoef));

            // See Mechanic doc for clarify formula
            value = unroundValue = eap / (eca * asi / dc + eia);

            return calculationReport;
        }
    }
}
