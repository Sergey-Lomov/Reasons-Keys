using ModelAnalyzer.Services;
using ModelAnalyzer.Parameters.Mining;

namespace ModelAnalyzer.Parameters.Activities
{
    class EventImpactPriceAU : FloatSingleParameter
    {
        public EventImpactPriceAU()
        {
            type = ParameterType.Out;
            title = "Стоимость слабого воздействия на событие (ЕА)";
            details = "Слабое воздействие - воздействие имеющее стандартный эффект (х1)";
            fractionalDigits = 2;
            ignoreRoundingIssue = true;
            tags.Add(ParameterTag.activities);
            tags.Add(ParameterTag.events);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float eeip = RequestParameter<EstimatedEventImpactPrice>(calculator).GetValue();
            float am = RequestParameter<AverageMining>(calculator).GetValue();
            float aupp = RequestParameter<AUPriceProportion>(calculator).GetValue();

            if (!calculationReport.IsSuccess)
                return calculationReport;

            unroundValue = eeip * aupp / am;
            value = (float)System.Math.Round(unroundValue);

            return calculationReport;
        }
    }
}
