using ModelAnalyzer.Services;
using ModelAnalyzer.Parameters.Mining;

namespace ModelAnalyzer.Parameters.Activities
{
    class EventImpact2PriceEU : FloatSingleParameter
    {
        private const int multyplier = 2;

        public EventImpact2PriceEU()
        {
            type = ParameterType.Out;
            title = "Стоимость среднего воздействия на событие (ТЗ)";
            details = "Среднее воздействие - воздействие имеющее двойной эффект";
            fractionalDigits = 0;
            tags.Add(ParameterTag.activities);
            tags.Add(ParameterTag.events);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float eeip = RequestParameter<EstimatedEventImpactPrice>(calculator).GetValue();
            float ei2pau = RequestParameter<EventImpact2PriceAU>(calculator).GetValue();
            float am = RequestParameter<AverageMining>(calculator).GetValue();

            if (!calculationReport.IsSuccess)
                return calculationReport;

            unroundValue = eeip * multyplier - ei2pau * am;
            value = (float)System.Math.Round(unroundValue);

            return calculationReport;
        }
    }
}