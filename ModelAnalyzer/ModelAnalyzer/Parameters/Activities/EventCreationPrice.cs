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
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float am = calculator.UpdatedParameter<AverageMining>().GetValue();
            float eсp_au = calculator.UpdatedParameter<EventCreationPriceAU>().GetValue();
            float eсp_eu = calculator.UpdatedParameter<EventCreationPriceEU>().GetValue();

            // See Mechanic doc for clarify formula
            value = unroundValue = eсp_au * am + eсp_eu;

            return calculationReport;
        }
    }
}
