using ModelAnalyzer.Services;
using ModelAnalyzer.Parameters.Mining;

namespace ModelAnalyzer.Parameters.Activities
{
    class EventCreationPriceEU : FloatSingleParameter
    {
        public EventCreationPriceEU()
        {
            type = ParameterType.Out;
            title = "Стоимость организации события (ТЗ)";
            details = "";
            fractionalDigits = 0;
            tags.Add(ParameterTag.activities);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float eecp = calculator.UpdatedParameter<EstimatedEventCreationPrice>().GetValue();
            float ecpau = calculator.UpdatedParameter<EventCreationPriceAU>().GetValue();
            float am = calculator.UpdatedParameter<AverageMining>().GetValue();

            unroundValue = eecp - ecpau * am;
            value = (float)System.Math.Round(unroundValue);

            return calculationReport;
        }
    }
}
