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
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float asi = calculator.UpdatedParameter<AverageStabilityIncrement>().GetValue();
            float am = calculator.UpdatedParameter<AverageMining>().GetValue();
            float eap = calculator.UpdatedParameter<EventsActionsPotential>().GetValue();
            float eca = calculator.UpdatedParameter<EventCreationAmount>().GetValue();
            float eia = calculator.UpdatedParameter<EventImpactAmount>().GetValue();
            float dc = calculator.UpdatedParameter<DestructionCoef>().GetValue();

            // See Mechanic doc for clarify formula
            value = unroundValue = eap / (eca * asi / dc + eia);

            return calculationReport;
        }
    }
}
