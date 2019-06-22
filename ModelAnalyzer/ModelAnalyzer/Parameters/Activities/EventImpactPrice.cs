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

            float asi = calculator.GetUpdateSingleValue(typeof(AverageStabilityIncrement));
            float am = calculator.GetUpdateSingleValue(typeof(AverageMining));
            float eap = calculator.GetUpdateSingleValue(typeof(EventsActionsPotential));
            float eca = calculator.GetUpdateSingleValue(typeof(EventCreationAmount));
            float eia = calculator.GetUpdateSingleValue(typeof(EventImpactAmount));
            float dc = calculator.GetUpdateSingleValue(typeof(DestructionCoef));

            // See Mechanic doc for clarify formula
            value = unroundValue = eap / (eca * asi / dc + eia);

            return calculationReport;
        }
    }
}
