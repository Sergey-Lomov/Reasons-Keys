using ModelAnalyzer.Services;
using ModelAnalyzer.Parameters.Mining;

namespace ModelAnalyzer.Parameters.Activities
{
    class EventCreationPriceAU : FloatSingleParameter
    {
        public EventCreationPriceAU()
        {
            type = ParameterType.Out;
            title = "Стоимость организации события (ЕА)";
            details = "";
            fractionalDigits = 2;
            tags.Add(ParameterTag.activities);
            tags.Add(ParameterTag.events);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float eecp = RequestParameter<EstimatedEventCreationPrice>(calculator).GetValue();
            float am = RequestParameter<AverageMining>(calculator).GetValue();
            float aupp = RequestParameter<AUPriceProportion>(calculator).GetValue();

            if (!calculationReport.IsSuccess)
                return calculationReport;

            unroundValue = eecp * aupp / am;
            float roundValue = (float)System.Math.Round(unroundValue);
            float timesPerAction = (float)System.Math.Round(1 / unroundValue);
            value = unroundValue < 1 ? 1 / timesPerAction : roundValue;

            return calculationReport;
        }
    }
}
