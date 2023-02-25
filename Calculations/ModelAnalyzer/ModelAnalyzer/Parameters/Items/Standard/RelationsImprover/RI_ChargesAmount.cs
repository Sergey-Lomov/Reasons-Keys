using ModelAnalyzer.Services;

using ModelAnalyzer.Parameters.Activities;
using System;

namespace ModelAnalyzer.Parameters.Items.Standard.RelationsImprover
{
    class RI_ChargesAmount : FloatSingleParameter
    {
        public RI_ChargesAmount()
        {
            type = ParameterType.Out;
            title = "УС: кол-во зарядов";
            details = "";
            fractionalDigits = 0;
            tags.Add(ParameterTag.items);
            tags.Add(ParameterTag.baseItems);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float apipr = RequestParameter<AveragePrimeItemsProfit>(calculator).GetValue();
            float eip = RequestParameter<EventImpactPrice>(calculator).GetValue();

            if (!calculationReport.IsSuccess)
                return calculationReport;

            unroundValue = apipr / eip;
            value = (float)Math.Round(unroundValue, MidpointRounding.AwayFromZero);

            return calculationReport;
        }
    }
}
