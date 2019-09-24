using ModelAnalyzer.Parameters.Moving;
using ModelAnalyzer.Parameters.Mining;

namespace ModelAnalyzer.Parameters.Items.Standard.SpeedBooster
{
    class SB_UpgradesProfit : ArrayParameter
    {
        public SB_UpgradesProfit()
        {
            type = ParameterType.Inner;
            title = "Ускоритель: относительная выгодность улучшений";
            details = "Выгодность улучшений оценивается исходя из того, что игрок не использует других возмжоностей для ускорения, кроме базового ускорителя";
            fractionalDigits = 2;
            tags.Add(ParameterTag.items);
            tags.Add(ParameterTag.moving);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float sdr = calculator.UpdatedSingleValue(typeof(SpeedDoublingRate));
            float am = calculator.UpdatedSingleValue(typeof(AverageMining));
            float isp = calculator.UpdatedSingleValue(typeof(InitialSpeed));
            float ua = calculator.UpdatedSingleValue(typeof(SB_UpgradesAmount));
            float[] sbp = calculator.UpdatedArrayValue(typeof(SB_Power));

            unroundValues.Clear();
            values.Clear();

            float prevUpgradeSpeed = isp;
            for (int i = 0; i < ua; i++)
            {
                float speed = prevUpgradeSpeed + sbp[i];
                float speedCoefficient = speed / prevUpgradeSpeed;
                float profit = (speedCoefficient - 1) * sdr * am;
                unroundValues.Add(profit);

                prevUpgradeSpeed = speed;
            }

            values = unroundValues;

            return calculationReport;
        }
    }
}
