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
            tags.Add(ParameterTag.events);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float eeip = RequestParmeter<EstimatedEventImpactPrice>(calculator).GetValue();
            float am = RequestParmeter<AverageMining>(calculator).GetValue();
            float aupp = RequestParmeter<AUPriceProportion>(calculator).GetValue();

            if (!calculationReport.IsSuccess)
                return calculationReport;

            unroundValue = eeip * aupp / am;
            float timesPerAction = (float)System.Math.Round(1 / unroundValue);
            value = 1 / timesPerAction;

            return calculationReport;
        }
    }
}
