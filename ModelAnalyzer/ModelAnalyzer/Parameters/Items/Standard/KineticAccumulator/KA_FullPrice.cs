using System;

using ModelAnalyzer.Services;

namespace ModelAnalyzer.Parameters.Items.Standard.KineticAccumulator
{
    class KA_FullPrice : FloatSingleParameter
    {
        public KA_FullPrice()
        {
            type = ParameterType.Out;
            title = "Накопитель ТЗ: полная стоимость";
            details = "";
            fractionalDigits = 2;
            tags.Add(ParameterTag.items);
            tags.Add(ParameterTag.baseItems);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float ipc = RequestParmeter<ItemPriceCoefficient>(calculator).GetValue();
            float pr = RequestParmeter<KA_Profit>(calculator).GetValue();

            unroundValue = ipc * pr;
            value = (float)Math.Round(unroundValue, MidpointRounding.AwayFromZero);

            return calculationReport;
        }
    }
}
