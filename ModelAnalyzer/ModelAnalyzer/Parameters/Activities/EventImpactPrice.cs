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
            tags.Add(ParameterTag.activities);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float am = calculator.UpdatedSingleValue(typeof(AverageMining));
            float eip_au = calculator.UpdatedSingleValue(typeof(EventImpactPriceAU));
            float eip_eu = calculator.UpdatedSingleValue(typeof(EventImpactPriceEU));

            // See Mechanic doc for clarify formula
            value = unroundValue = eip_au * am + eip_eu;

            return calculationReport;
        }
    }
}
