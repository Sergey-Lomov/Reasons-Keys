using ModelAnalyzer.Parameters.Mining;

namespace ModelAnalyzer.Parameters.Activities
{
    class EventCreationPriceAU : SingleParameter
    {
        public EventCreationPriceAU()
        {
            type = ParameterType.Out;
            title = "Стоимость организации события (ЕА)";
            details = "";
            fractionalDigits = 2;
            tags.Add(ParameterTag.activities);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float ecp = calculator.UpdatedSingleValue(typeof(EventCreationPrice));
            float am = calculator.UpdatedSingleValue(typeof(AverageMining));
            float aupp = calculator.UpdatedSingleValue(typeof(AUPriceProportion));

            unroundValue = ecp * aupp / am;
            float timesPerAction = (float)System.Math.Round(1 / unroundValue);
            value = 1 / timesPerAction;

            return calculationReport;
        }
    }
}
