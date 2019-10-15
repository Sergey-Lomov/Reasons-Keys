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

            float eeip = calculator.UpdatedParameter<EstimatedEventImpactPrice>().GetValue();
            float eipau = calculator.UpdatedParameter<EventImpactPriceAU>().GetValue();
            float am = calculator.UpdatedParameter<AverageMining>().GetValue();

            unroundValue = eeip - eipau * am;
            value = (float) System.Math.Round(unroundValue);

            return calculationReport;
        }
    }
}
