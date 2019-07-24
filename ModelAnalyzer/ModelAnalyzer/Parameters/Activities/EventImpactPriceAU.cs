using ModelAnalyzer.Parameters.Mining;

namespace ModelAnalyzer.Parameters.Activities
{
    class EventImpactPriceAU : SingleParameter
    {
        public EventImpactPriceAU()
        {
            type = ParameterType.Out;
            title = "Стоимость воздействия на событие (ЕА)";
            details = "";
            fractionalDigits = 2;
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float eip = calculator.UpdatedSingleValue(typeof(EventImpactPrice));
            float am = calculator.UpdatedSingleValue(typeof(AverageMining));
            float aupp = calculator.UpdatedSingleValue(typeof(AUPriceProportion));

            unroundValue = eip * aupp / am;
            float timesPerAction = (float)System.Math.Round(1 / unroundValue);
            value = 1 / timesPerAction;

            return calculationReport;
        }
    }
}
