using System;
using System.Collections.Generic;

using ModelAnalyzer.Services;

namespace ModelAnalyzer.Parameters.Items.Standard.BaseWeapon
{
    class BW_FullPrice : FloatArrayParameter
    {
        public BW_FullPrice()
        {
            type = ParameterType.Out;
            title = "БО: полная стоимость";
            details = "Полная стоимость БО и чистая стоимость улучшений (стоимость БО не входит в стоимость улучшений)";
            fractionalDigits = 0;
            tags.Add(ParameterTag.items);
            tags.Add(ParameterTag.baseItems);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float ipc = calculator.UpdatedParameter<ItemPriceCoefficient>().GetValue();
            List<float> up = calculator.UpdatedParameter<BW_UpgradesProfit>().GetValue();

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
