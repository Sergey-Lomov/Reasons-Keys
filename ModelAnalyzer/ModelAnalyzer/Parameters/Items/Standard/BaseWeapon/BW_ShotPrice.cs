using System;
using System.Collections.Generic;

using ModelAnalyzer.Services;

namespace ModelAnalyzer.Parameters.Items.Standard.BaseWeapon
{
    class BW_ShotPrice : FloatArrayParameter
    {
        public BW_ShotPrice()
        {
            type = ParameterType.Out;
            title = "БО: стоимость залпа";
            details = "Стоимость одного выстрела из БО (при разном кол-ве улучшений)";
            fractionalDigits = 0;
            tags.Add(ParameterTag.items);
            tags.Add(ParameterTag.baseItems);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float wse = calculator.UpdatedParameter<WeaponStandardEffectivity>().GetValue();
            float mec = calculator.UpdatedParameter<BW_MaxEffectivityCoefficient>().GetValue();
            float ua = calculator.UpdatedParameter<BW_UpgradesAmount>().GetValue();
            List<float> bwd = calculator.UpdatedParameter<BW_Damage>().GetValue();

            unroundValues.Clear();
            values.Clear();

            float step = wse * (mec - 1) / ua;
            for (int i = 0; i <= (int)ua; i++)
            {
                float effectivity = wse + i * step;
                unroundValues.Add(bwd[i] / effectivity);
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
