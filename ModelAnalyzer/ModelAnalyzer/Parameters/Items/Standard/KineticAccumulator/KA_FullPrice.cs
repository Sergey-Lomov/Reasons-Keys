using System;

namespace ModelAnalyzer.Parameters.Items.Standard.KineticAccumulator
{
    class KA_FullPrice : SingleParameter
    {
        public KA_FullPrice()
        {
            type = ParameterType.Out;
            title = "Накопитель ТЗ: полная стоимость";
            details = "";
            fractionalDigits = 2;
            tags.Add(ParameterTag.items);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float ipc = calculator.UpdatedSingleValue(typeof(ItemPriceCoefficient));
            float pr = calculator.UpdatedSingleValue(typeof(KA_Profit));

            unroundValue = ipc * pr;
            value = (float)Math.Round(unroundValue, MidpointRounding.AwayFromZero);

            return calculationReport;
        }
    }
}
