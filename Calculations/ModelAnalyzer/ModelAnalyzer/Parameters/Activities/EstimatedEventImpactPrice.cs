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

            float arip = RequestParameter<AverageRelationsImpactPower>(calculator).GetValue();
            float eap = RequestParameter<EventsActionsPotential>(calculator).GetValue();
            float eca = RequestParameter<EventCreationAmount>(calculator).GetValue();
            float eia = RequestParameter<EventImpactAmount>(calculator).GetValue();
            float dc = RequestParameter<DestructionCoef>(calculator).GetValue();

            if (!calculationReport.IsSuccess)
                return calculationReport;

            // See Mechanic doc for clarify formula
            value = unroundValue = eap / (eca * arip * dc + eia);

            return calculationReport;
        }
    }
}
