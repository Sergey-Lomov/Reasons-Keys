using System.Collections.Generic;

using ModelAnalyzer.Services;
using ModelAnalyzer.Parameters.Moving;
using ModelAnalyzer.Parameters.Mining;

namespace ModelAnalyzer.Parameters.Items.Standard.SpeedBooster
{
    class SB_UpgradesProfit : FloatArrayParameter
    {
        public SB_UpgradesProfit()
        {
            type = ParameterType.Inner;
            title = "Ускоритель: относительная выгодность улучшений";
            details = "Выгодность улучшений оценивается исходя из того, что игрок не использует других возмжоностей для ускорения, кроме базового ускорителя";
            fractionalDigits = 2;
            tags.Add(ParameterTag.items);
            tags.Add(ParameterTag.baseItems);
            tags.Add(ParameterTag.moving);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float sdr = RequestParmeter<SpeedDoublingRate>(calculator).GetValue();
            float am = RequestParmeter<AverageMining>(calculator).GetValue();
            float isp = RequestParmeter<InitialSpeed>(calculator).GetValue();
            float ua = RequestParmeter<SB_UpgradesAmount>(calculator).GetValue();
            List<float> sbp = RequestParmeter<SB_Power>(calculator).GetValue();

            if (!calculationReport.IsSuccess)
                return calculationReport;

            unroundValues = new List<float>();
            values = new List<float>();

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
