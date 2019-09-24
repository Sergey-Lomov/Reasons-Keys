using System;

namespace ModelAnalyzer.Parameters.Items.Standard.BaseWeapon
{
    class BW_ShotPrice : ArrayParameter
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

            float wse = calculator.UpdatedSingleValue(typeof(WeaponStandardEffectivity));
            float mec = calculator.UpdatedSingleValue(typeof(BW_MaxEffectivityCoefficient));
            float ua = calculator.UpdatedSingleValue(typeof(BW_UpgradesAmount));
            float[] bwd = calculator.UpdatedArrayValue(typeof(BW_Damage));

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
