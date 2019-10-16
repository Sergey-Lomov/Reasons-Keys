using ModelAnalyzer.Services;
using ModelAnalyzer.Parameters.Mining;

namespace ModelAnalyzer.Parameters.Activities
{
    class EventImpactPriceEU : FloatSingleParameter
    {
        public EventImpactPriceEU()
        {
            type = ParameterType.Out;
            title = "Стоимость воздействия на событие (ТЗ)";
            details = "";
            fractionalDigits = 0;
            tags.Add(ParameterTag.activities);
            tags.Add(ParameterTag.events);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float eeip = RequestParmeter<EstimatedEventImpactPrice>(calculator).GetValue();
            float eipau = RequestParmeter<EventImpactPriceAU>(calculator).GetValue();
            float am = RequestParmeter<AverageMining>(calculator).GetValue();

            if (!calculationReport.IsSuccess)
                return calculationReport;

            unroundValue = eeip - eipau * am;
            value = (float) System.Math.Round(unroundValue);

            return calculationReport;
        }
    }
}
