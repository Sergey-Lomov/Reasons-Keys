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
            tags.Add(ParameterTag.activities);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float eip = calculator.UpdatedSingleValue(typeof(EventImpactPrice));
            float eipau = calculator.UpdatedSingleValue(typeof(EventImpactPriceAU));
            float am = calculator.UpdatedSingleValue(typeof(AverageMining));

            unroundValue = eip - eipau * am;
            value = (float) System.Math.Round(unroundValue);

            return calculationReport;
        }
    }
}
