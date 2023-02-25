using ModelAnalyzer.Services;
using ModelAnalyzer.Parameters.Mining;

namespace ModelAnalyzer.Parameters.Activities
{
    class EventImpact3PriceEU : FloatSingleParameter
    {
        private const int multyplier = 3;

        public EventImpact3PriceEU()
        {
            type = ParameterType.Out;
            title = "Стоимость мощного воздействия на событие (ТЗ)";
            details = "Мощное воздействие - воздействие имеющее тройной эффект";
            fractionalDigits = 0;
            tags.Add(ParameterTag.activities);
            tags.Add(ParameterTag.events);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float eeip = RequestParameter<EstimatedEventImpactPrice>(calculator).GetValue();
            float ei3pau = RequestParameter<EventImpact3PriceAU>(calculator).GetValue();
            float am = RequestParameter<AverageMining>(calculator).GetValue();

            if (!calculationReport.IsSuccess)
                return calculationReport;

            unroundValue = eeip * multyplier - ei3pau * am;
            value = (float)System.Math.Round(unroundValue);

            return calculationReport;
        }
    }
}