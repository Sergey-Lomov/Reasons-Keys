﻿using System.Linq;
using System.Collections.Generic;

using ModelAnalyzer.Services;

namespace ModelAnalyzer.Parameters.Items.Standard.SpeedBooster
{
    class SB_Profit : FloatSingleParameter
    {
        public SB_Profit()
        {
            type = ParameterType.Inner;
            title = "Ускоритель: выгодность";
            details = "Средняя полная выгодность всех ступеней ускорителя";
            fractionalDigits = 2;
            tags.Add(ParameterTag.items);
            tags.Add(ParameterTag.baseItems);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            List<float> up = RequestParmeter<SB_UpgradesProfit>(calculator).GetValue();
            if (!calculationReport.IsSuccess)
                return calculationReport;

            if (up.Count() == 0)
            {
                value = unroundValue = 0;
                return calculationReport;
            }

            var upgradesFullProfit = new List<float>();
            for (int i = 0; i < up.Count(); i++)
            {
                float upgradeFullProfit = 0;
                for (int j = 0; j <= i; j++)
                    upgradeFullProfit += up[j];
                upgradesFullProfit.Add(upgradeFullProfit);
            }

            value = unroundValue = upgradesFullProfit.Average();

            return calculationReport;
        }
    }
}
