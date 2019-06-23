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

            float asi = calculator.UpdateSingleValue(typeof(AverageStabilityIncrement));
            float eap = calculator.UpdateSingleValue(typeof(EventsActionsPotential));
            float eca = calculator.UpdateSingleValue(typeof(EventCreationAmount));
            float eia = calculator.UpdateSingleValue(typeof(EventImpactAmount));
            float eip = calculator.UpdateSingleValue(typeof(EventImpactPrice));
            float dc = calculator.UpdateSingleValue(typeof(DestructionCoef));

            // See Mechanic doc for clarify formulas
            float ecp1 = eip * asi / dc; //Rule 1
            float ecp2 = (eap - eia * eip) / eca; //Rule 2
            value = unroundValue = (ecp1 + ecp2) / 2;

            return calculationReport;
        }
    }
}
