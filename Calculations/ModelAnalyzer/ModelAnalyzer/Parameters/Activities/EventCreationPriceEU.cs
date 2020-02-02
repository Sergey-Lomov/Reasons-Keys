using ModelAnalyzer.Services;
using ModelAnalyzer.Parameters.Mining;

namespace ModelAnalyzer.Parameters.Activities
{
    class EventCreationPriceEU : FloatSingleParameter
    {
        public EventCreationPriceEU()
        {
            type = ParameterType.Out;
            title = "Стоимость организации события (ТЗ)";
            details = "";
            fractionalDigits = 0;
            tags.Add(ParameterTag.activities);
            tags.Add(ParameterTag.events);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float eecp = RequestParmeter<EstimatedEventCreationPrice>(calculator).GetValue();
            float ecpau = RequestParmeter<EventCreationPriceAU>(calculator).GetValue();
            float am = RequestParmeter<AverageMining>(calculator).GetValue();

            if (!calculationReport.IsSuccess)
                return calculationReport;

            unroundValue = eecp - ecpau * am;
            value = (float)System.Math.Round(unroundValue);

            return calculationReport;
        }
    }
}
