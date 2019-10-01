using ModelAnalyzer.Parameters.Mining;

namespace ModelAnalyzer.Parameters.Activities
{
    class EventCreationPriceEU : SingleParameter
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

            float eecp = calculator.UpdatedSingleValue(typeof(EstimatedEventCreationPrice));
            float ecpau = calculator.UpdatedSingleValue(typeof(EventCreationPriceAU));
            float am = calculator.UpdatedSingleValue(typeof(AverageMining));

            unroundValue = eecp - ecpau * am;
            value = (float)System.Math.Round(unroundValue);

            return calculationReport;
        }
    }
}
