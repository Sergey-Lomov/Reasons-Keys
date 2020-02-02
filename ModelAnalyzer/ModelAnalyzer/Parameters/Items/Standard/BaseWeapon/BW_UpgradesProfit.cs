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

            float saa = RequestParmeter<AtackAmount>(calculator).GetValue();
            float ua = RequestParmeter<BW_UpgradesAmount>(calculator).GetValue();
            List<float> bwd = RequestParmeter<BW_Damage>(calculator).GetValue();
            List<float> bwsp = RequestParmeter<BW_ShotPrice>(calculator).GetValue();
            List<float> bsd = RequestParmeter<BS_Defense>(calculator).GetValue();

            if (!calculationReport.IsSuccess)
                return calculationReport;
            ClearValues();

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
