using System.Collections.Generic;

using ModelAnalyzer.Services;
using ModelAnalyzer.Parameters.Activities;
using ModelAnalyzer.Parameters.Items.Standard.BaseShield;

namespace ModelAnalyzer.Parameters.Items.Standard.BaseWeapon
{
    class BW_UpgradesProfit : FloatArrayParameter
    {
        public BW_UpgradesProfit()
        {
            type = ParameterType.Inner;
            title = "БО: полная выгодность улучшений";
            details = "Полная выгодность БО с различным кол-во улучшений (включая и БО без улучшений)";
            fractionalDigits = 2;
            tags.Add(ParameterTag.items);
            tags.Add(ParameterTag.baseItems);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float saa = calculator.UpdatedParameter<StandardAtackAmount>().GetValue();
            float ua = calculator.UpdatedParameter<BW_UpgradesAmount>().GetValue();
            List<float> bwd = calculator.UpdatedParameter<BW_Damage>().GetValue();
            List<float> bwsp = calculator.UpdatedParameter<BW_ShotPrice>().GetValue();
            List<float> bsd = calculator.UpdatedParameter<BS_Defense>().GetValue();

            unroundValues.Clear();
            values.Clear();

            for (var i = 0; i <= ua; i++)
            {
                var profit = (bwd[i] - bwsp[i] - bsd[i]) * saa;
                unroundValues.Add(profit);
            }

            values = unroundValues;

            return calculationReport;
        }
    }
}
