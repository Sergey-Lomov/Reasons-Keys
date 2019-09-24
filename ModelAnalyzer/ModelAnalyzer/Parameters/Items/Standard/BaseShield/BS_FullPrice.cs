using System;

namespace ModelAnalyzer.Parameters.Items.Standard.BaseShield
{
    class BS_FullPrice : ArrayParameter
    {
        public BS_FullPrice()
        {
            type = ParameterType.Out;
            title = "БЩ: полная стоимость";
            details = "Полнач стоимость БЩ и его улучшений (чистая стоимость улучшений - без стоимости БЩ, и предыдущих улучшений)";
            fractionalDigits = 0;
            tags.Add(ParameterTag.items);
            tags.Add(ParameterTag.baseItems);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float ipc = calculator.UpdatedSingleValue(typeof(ItemPriceCoefficient));
            float[] up = calculator.UpdatedArrayValue(typeof(BS_UpgradesProfit));

            unroundValues.Clear();
            values.Clear();

            float previousUpgradeProfit = 0;
            foreach (var profit in up)
            {
                var value = ipc * (profit - previousUpgradeProfit);
                var rounded = Math.Round(value, MidpointRounding.AwayFromZero);
                unroundValues.Add(value);
                values.Add((float)rounded);

                previousUpgradeProfit = profit;
            }

            return calculationReport;
        }
    }
}
