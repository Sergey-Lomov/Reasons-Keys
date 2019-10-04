using ModelAnalyzer.Services;
using ModelAnalyzer.Parameters.Mining;

namespace ModelAnalyzer.Parameters.Activities
{
    class EventImpactPrice : FloatSingleParameter
    {
        public EventImpactPrice()
        {
            type = ParameterType.Inner;
            title = "Полная стоимость воздействия на событие";
            details = "";
            fractionalDigits = 2;
            tags.Add(ParameterTag.activities);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float am = calculator.UpdatedParameter<AverageMining>().GetValue();
            float eip_au = calculator.UpdatedParameter<EventImpactPriceAU>().GetValue();
            float eip_eu = calculator.UpdatedParameter<EventImpactPriceEU>().GetValue();

            // See Mechanic doc for clarify formula
            value = unroundValue = eip_au * am + eip_eu;

            return calculationReport;
        }
    }
}
