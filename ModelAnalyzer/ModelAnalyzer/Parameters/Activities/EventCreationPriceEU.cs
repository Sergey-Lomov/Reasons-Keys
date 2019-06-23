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
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float ecp = calculator.UpdateSingleValue(typeof(EventCreationPrice));
            float ecpau = calculator.UpdateSingleValue(typeof(EventCreationPriceAU));
            float am = calculator.UpdateSingleValue(typeof(AverageMining));

            unroundValue = ecp - ecpau * am;
            value = (float)System.Math.Round(unroundValue);

            return calculationReport;
        }
    }
}
