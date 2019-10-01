using System;

namespace ModelAnalyzer.Parameters.Items.Standard.SpeedBooster
{
    class SB_FullPrice : ArrayParameter
    {
        public SB_FullPrice()
        {
            type = ParameterType.Out;
            title = "Ускоритель: полная стоимость";
            details = "";
            fractionalDigits = 0;
            tags.Add(ParameterTag.items);
            tags.Add(ParameterTag.baseItems);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float ipc = calculator.UpdatedSingleValue(typeof(ItemPriceCoefficient));
            float[] up = calculator.UpdatedArrayValue(typeof(SB_UpgradesProfit));

            unroundValues.Clear();
            values.Clear();

            foreach (var profit in up)
            {
                var price = ipc * profit;
                var roundedPrice = Math.Round(price, MidpointRounding.AwayFromZero);
                unroundValues.Add(price);
                values.Add((float)roundedPrice);
            }

            return calculationReport;
        }

        internal override ParameterValidationReport Validate(Validator validator, Storage storage)
        {
            var report = base.Validate(validator, storage);
            var size = storage.Parameter(typeof(SB_UpgradesAmount));
            ValidateSize(size, report);
            return report;
        }
    }
}
