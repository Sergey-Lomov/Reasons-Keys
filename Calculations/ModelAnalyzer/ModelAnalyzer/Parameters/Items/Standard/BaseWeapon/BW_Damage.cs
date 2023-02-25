using System;

using ModelAnalyzer.Services;
using ModelAnalyzer.Parameters.Mining;
using ModelAnalyzer.Parameters.Events;

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

            float wsp = RequestParameter<WeaponStandardPower>(calculator).GetValue();
            float mpc = RequestParameter<BW_MaxPowerCoefficient>(calculator).GetValue();
            float ua = RequestParameter<BW_UpgradesAmount>(calculator).GetValue();
            float am = RequestParameter<AverageMining>(calculator).GetValue();
            float amb = RequestParameter<AverageMiningBonus>(calculator).GetValue();

            if (!calculationReport.IsSuccess)
                return calculationReport;
            ClearValues();

            float step = wsp * (mpc - 1) / ua;
            for (int i = 0; i <= (int)ua; i++)
            {
                float power = wsp + i * step;
                unroundValues.Add(power * (am + amb));
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
