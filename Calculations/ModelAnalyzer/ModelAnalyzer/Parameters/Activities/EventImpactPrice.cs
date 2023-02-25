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
            tags.Add(ParameterTag.events);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float am = RequestParameter<AverageMining>(calculator).GetValue();
            float eip_au = RequestParameter<EventImpactPriceAU>(calculator).GetValue();
            float eip_eu = RequestParameter<EventImpactPriceEU>(calculator).GetValue();

            if (!calculationReport.IsSuccess)
                return calculationReport;

            // See Mechanic doc for clarify formula
            value = unroundValue = eip_au * am + eip_eu;

            return calculationReport;
        }
    }
}
