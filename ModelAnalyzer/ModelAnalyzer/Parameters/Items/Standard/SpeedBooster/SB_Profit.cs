using System.Linq;
using System.Collections.Generic;

namespace ModelAnalyzer.Parameters.Items.Standard.SpeedBooster
{
    class SB_Profit : SingleParameter
    {
        public SB_Profit()
        {
            type = ParameterType.Inner;
            title = "Ускоритель: выгодность";
            details = "Средняя полная выгодность всех ступеней ускорителя";
            fractionalDigits = 2;
            tags.Add(ParameterTag.items);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float[] up = calculator.UpdatedArrayValue(typeof(SB_UpgradesProfit));

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
