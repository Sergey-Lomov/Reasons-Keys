using System;

using ModelAnalyzer.Services;

namespace ModelAnalyzer.Parameters.Items.Standard.RelationsImprover
{
    class RI_FullPrice : FloatSingleParameter
    {
        public RI_FullPrice()
        {
            type = ParameterType.Out;
            title = "УС: полная стоимость";
            details = "";
            fractionalDigits = 0;
            tags.Add(ParameterTag.items);
            tags.Add(ParameterTag.baseItems);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float ipc = RequestParmeter<ItemPriceCoefficient>(calculator).GetValue();
            float pr = RequestParmeter<RI_Profit>(calculator).GetValue();

            if (!calculationReport.IsSuccess)
                return calculationReport;

            unroundValue = pr * ipc;
            value = (float)Math.Round(unroundValue, MidpointRounding.AwayFromZero);

            return calculationReport;
        }
    }
}
