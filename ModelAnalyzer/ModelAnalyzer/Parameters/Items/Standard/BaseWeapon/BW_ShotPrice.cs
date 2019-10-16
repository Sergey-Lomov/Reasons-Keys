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

            float wse = RequestParmeter<WeaponStandardEffectivity>(calculator).GetValue();
            float mec = RequestParmeter<BW_MaxEffectivityCoefficient>(calculator).GetValue();
            float ua = RequestParmeter<BW_UpgradesAmount>(calculator).GetValue();
            List<float> bwd = RequestParmeter<BW_Damage>(calculator).GetValue();

            if (!calculationReport.IsSuccess)
                return calculationReport;

            unroundValues = new List<float>();
            values = new List<float>();

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
