using System;
using System.Collections.Generic;

using ModelAnalyzer.Services;
using ModelAnalyzer.Parameters.Mining;

namespace ModelAnalyzer.Parameters.Items.Standard.BaseWeapon
{
    class BW_Damage : FloatArrayParameter
    {
        public BW_Damage()
        {
            type = ParameterType.Out;
            title = "БО: урон";
            details = "Непосредственный урон от испольозвания БО, получаемый проитвником без щитов.";
            fractionalDigits = 0;
            tags.Add(ParameterTag.items);
            tags.Add(ParameterTag.baseItems);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float wsp = RequestParmeter<WeaponStandardPower>(calculator).GetValue();
            float mpc = RequestParmeter<BW_MaxPowerCoefficient>(calculator).GetValue();
            float ua = RequestParmeter<BW_UpgradesAmount>(calculator).GetValue();
            float am = RequestParmeter<AverageMining>(calculator).GetValue();

            if (!calculationReport.IsSuccess)
                return calculationReport;

            unroundValues = new List<float>();
            values = new List<float>();

            float step = wsp * (mpc - 1) / ua;
            for (int i = 0; i <= (int)ua; i++)
            {
                float power = wsp + i * step;
                unroundValues.Add(power * am);
            }

            foreach (var value in unroundValues)
            {
                var rounded = Math.Round(value, MidpointRounding.AwayFromZero);
                values.Add((float)rounded);
            }

            return calculationReport;
        }
    }
}
