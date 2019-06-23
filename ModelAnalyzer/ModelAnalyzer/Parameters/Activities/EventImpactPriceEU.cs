using ModelAnalyzer.Parameters.Mining;

namespace ModelAnalyzer.Parameters.Activities
{
    class EventImpactPriceEU : SingleParameter
    {
        public EventImpactPriceEU()
        {
            type = ParameterType.Out;
            title = "Стоимость воздействия на событие (ТЗ)";
            details = "";
            fractionalDigits = 0;
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float eip = calculator.UpdateSingleValue(typeof(EventImpactPrice));
            float eipau = calculator.UpdateSingleValue(typeof(EventImpactPriceAU));
            float am = calculator.UpdateSingleValue(typeof(AverageMining));

            unroundValue = eip - eipau * am;
            value = (float) System.Math.Round(unroundValue);

            return calculationReport;
        }
    }
}
