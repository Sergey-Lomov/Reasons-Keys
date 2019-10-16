using ModelAnalyzer.Services;
using ModelAnalyzer.Parameters.Mining;

namespace ModelAnalyzer.Parameters.Activities
{
    class EventCreationPrice : FloatSingleParameter
    {
        public EventCreationPrice()
        {
            type = ParameterType.Inner;
            title = "Полная стоимость организации события";
            details = "";
            fractionalDigits = 2;
            tags.Add(ParameterTag.activities);
            tags.Add(ParameterTag.events);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float am = RequestParmeter<AverageMining>(calculator).GetValue();
            float eсp_au = RequestParmeter<EventCreationPriceAU>(calculator).GetValue();
            float eсp_eu = RequestParmeter<EventCreationPriceEU>(calculator).GetValue();

            if (!calculationReport.IsSuccess)
                return calculationReport;

            // See Mechanic doc for clarify formula
            value = unroundValue = eсp_au * am + eсp_eu;

            return calculationReport;
        }
    }
}
