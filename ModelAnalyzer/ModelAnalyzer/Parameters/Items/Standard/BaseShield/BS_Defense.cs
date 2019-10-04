using System;
using System.Collections.Generic;

using ModelAnalyzer.Services;
using ModelAnalyzer.Parameters.Items.Standard.BaseWeapon;

namespace ModelAnalyzer.Parameters.Items.Standard.BaseShield
{
    class BS_Defense : FloatArrayParameter
    {
        public BS_Defense()
        {
            type = ParameterType.Out;
            title = "БЩ: защита";
            details = "Защита щита при различном-колве улучшений";
            fractionalDigits = 0;
            tags.Add(ParameterTag.items);
            tags.Add(ParameterTag.baseItems);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float bp = calculator.UpdatedParameter<BS_BasePower>().GetValue();
            float mp = calculator.UpdatedParameter<BS_MaxPower>().GetValue();
            float ua = calculator.UpdatedParameter<BW_UpgradesAmount>().GetValue();
            List<float> bwd = calculator.UpdatedParameter<BW_Damage>().GetValue();

            unroundValues.Clear();
            values.Clear();

            float step = (mp - bp) / ua;
            for (int i = 0; i <= (int)ua; i++)
            {
                float power = bp + i * step;
                unroundValues.Add(power * bwd[i]);
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
