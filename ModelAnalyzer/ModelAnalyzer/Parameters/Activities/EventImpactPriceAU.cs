using ModelAnalyzer.Services;
using ModelAnalyzer.Parameters.Mining;

namespace ModelAnalyzer.Parameters.Activities
{
    class EventImpactPriceAU : FloatSingleParameter
    {
        public EventImpactPriceAU()
        {
            type = ParameterType.Out;
            title = "Стоимость воздействия на событие (ЕА)";
            details = "";
            fractionalDigits = 2;
            tags.Add(ParameterTag.activities);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float eeip = calculator.UpdatedSingleValue(typeof(EstimatedEventImpactPrice));
            float am = calculator.UpdatedSingleValue(typeof(AverageMining));
            float aupp = calculator.UpdatedSingleValue(typeof(AUPriceProportion));

            unroundValue = eeip * aupp / am;
            float timesPerAction = (float)System.Math.Round(1 / unroundValue);
            value = 1 / timesPerAction;

            return calculationReport;
        }
    }
}
