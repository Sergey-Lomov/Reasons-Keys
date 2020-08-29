using ModelAnalyzer.Services;
using ModelAnalyzer.Parameters.Mining;

namespace ModelAnalyzer.Parameters.Activities
{
    class EventImpact3PriceAU : FloatSingleParameter
    {
        private const int multyplier = 3;

        public EventImpact3PriceAU()
        {
            type = ParameterType.Out;
            title = "Стоимость мощного воздействия на событие (ЕА)";
            details = "Мощное воздействие - воздействие имеющее тройной эффект";
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