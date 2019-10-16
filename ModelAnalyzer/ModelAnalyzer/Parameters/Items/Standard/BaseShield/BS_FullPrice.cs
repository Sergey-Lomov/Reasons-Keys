using System;
using System.Collections.Generic;
using ModelAnalyzer.Services;

namespace ModelAnalyzer.Parameters.Items.Standard.BaseShield
{
    class BS_FullPrice : FloatArrayParameter
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

            float ipc = RequestParmeter<ItemPriceCoefficient>(calculator).GetValue();
            List<float> up = RequestParmeter<BS_UpgradesProfit>(calculator).GetValue();

            if (!calculationReport.IsSuccess)
                return calculationReport;

            unroundValues = new List<float>();
            values = new List<float>();

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
