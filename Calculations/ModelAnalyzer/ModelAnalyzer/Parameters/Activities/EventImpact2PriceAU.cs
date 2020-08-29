using ModelAnalyzer.Services;
using ModelAnalyzer.Parameters.Mining;

namespace ModelAnalyzer.Parameters.Activities
{
    class EventImpact2PriceAU : FloatSingleParameter
    {
        private const int multyplier = 2;

        public EventImpact2PriceAU()
        {
            type = ParameterType.Out;
            title = "Стоимость среднего воздействия на событие (ЕА)";
            details = "Среднее воздействие - воздействие имеющее двойной эффект";
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

            unroundValue = eeip * multyplier * aupp / am;
            value = (float)System.Math.Round(unroundValue);

            return calculationReport;
        }
    }
}