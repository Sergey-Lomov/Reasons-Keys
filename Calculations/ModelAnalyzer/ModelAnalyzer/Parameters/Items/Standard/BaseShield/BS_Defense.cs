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

            float bp = RequestParameter<BS_BasePower>(calculator).GetValue();
            float mp = RequestParameter<BS_MaxPower>(calculator).GetValue();
            float ua = RequestParameter<BW_UpgradesAmount>(calculator).GetValue();
            List<float> bwd = RequestParameter<BW_Damage>(calculator).GetValue();

            if (!calculationReport.IsSuccess)
                return calculationReport;
            ClearValues();

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
