using ModelAnalyzer.Parameters.Events;
using ModelAnalyzer.Parameters.Mining;

namespace ModelAnalyzer.Parameters.Activities
{
    class EstimatedEventImpactPrice : SingleParameter
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
