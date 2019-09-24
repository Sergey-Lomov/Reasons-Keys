using ModelAnalyzer.Parameters.Activities;
using ModelAnalyzer.Parameters.Items.Standard.BaseShield;

namespace ModelAnalyzer.Parameters.Items.Standard.BaseWeapon
{
    class BW_UpgradesProfit : ArrayParameter
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

            float saa = calculator.UpdatedSingleValue(typeof(StandardAtackAmount));
            float ua = calculator.UpdatedSingleValue(typeof(BW_UpgradesAmount));
            float[] bwd = calculator.UpdatedArrayValue(typeof(BW_Damage));
            float[] bwsp = calculator.UpdatedArrayValue(typeof(BW_ShotPrice));
            float[] bsd = calculator.UpdatedArrayValue(typeof(BS_Defense));

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
