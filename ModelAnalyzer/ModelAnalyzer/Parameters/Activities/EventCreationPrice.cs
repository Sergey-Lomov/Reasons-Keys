using ModelAnalyzer.Parameters.Events;

namespace ModelAnalyzer.Parameters.Activities
{
    class EventCreationPrice : SingleParameter
    {
        public EventCreationPrice()
        {
            type = ParameterType.Inner;
            title = "Полная стоимость организации события";
            details = "";
            fractionalDigits = 2;
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float asi = calculator.UpdatedSingleValue(typeof(AverageStabilityIncrement));
            float eap = calculator.UpdatedSingleValue(typeof(EventsActionsPotential));
            float eca = calculator.UpdatedSingleValue(typeof(EventCreationAmount));
            float eia = calculator.UpdatedSingleValue(typeof(EventImpactAmount));
            float eip = calculator.UpdatedSingleValue(typeof(EventImpactPrice));
            float dc = calculator.UpdatedSingleValue(typeof(DestructionCoef));

            // See Mechanic doc for clarify formulas
            float ecp1 = eip * asi / dc; //Rule 1
            float ecp2 = (eap - eia * eip) / eca; //Rule 2
            value = unroundValue = (ecp1 + ecp2) / 2;

            return calculationReport;
        }
    }
}
