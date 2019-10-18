using ModelAnalyzer.Services;
using ModelAnalyzer.Parameters.Events;
using ModelAnalyzer.Parameters.Mining;

namespace ModelAnalyzer.Parameters.Activities
{
    class EstimatedEventImpactPrice : FloatSingleParameter
    {
        public EstimatedEventImpactPrice()
        {
            type = ParameterType.Inner;
            title = "Расчетная полная стоимость воздействия на событие";
            details = "Стоимость, которую должно было бы иметь воздействие на событие, если бы не нужно было округлять стоимость в ЕА и ТЗ";
            fractionalDigits = 2;
            tags.Add(ParameterTag.activities);
            tags.Add(ParameterTag.events);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float asi = RequestParmeter<AverageStabilityIncrement>(calculator).GetValue();
            float eap = RequestParmeter<EventsActionsPotential>(calculator).GetValue();
            float eca = RequestParmeter<EventCreationAmount>(calculator).GetValue();
            float eia = RequestParmeter<EventImpactAmount>(calculator).GetValue();
            float dc = RequestParmeter<DestructionCoef>(calculator).GetValue();

            if (!calculationReport.IsSuccess)
                return calculationReport;

            // See Mechanic doc for clarify formula
            value = unroundValue = eap / (eca * asi / dc + eia);

            return calculationReport;
        }
    }
}
