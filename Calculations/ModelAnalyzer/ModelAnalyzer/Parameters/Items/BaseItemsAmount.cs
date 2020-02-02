using ModelAnalyzer.Services;
using ModelAnalyzer.Parameters.Items.Standard.BaseWeapon;
using ModelAnalyzer.Parameters.Items.Standard.BaseShield;
using ModelAnalyzer.Parameters.Items.Standard.SpeedBooster;

namespace ModelAnalyzer.Parameters.Items
{
    class BaseItemsAmount : FloatSingleParameter
    {
        public BaseItemsAmount()
        {
            type = ParameterType.Inner;
            title = "Кол-во базовых предметов";
            details = "";
            fractionalDigits = 0;
            tags.Add(ParameterTag.items);
            tags.Add(ParameterTag.baseItems);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            var bwa = RequestParmeter<BW_UpgradesAmount>(calculator).GetValue();
            var sba = RequestParmeter<SB_UpgradesAmount>(calculator).GetValue();

            if (!calculationReport.IsSuccess)
                return calculationReport;

            unroundValue = bwa + 1;     // Base weapon and upgrades
            unroundValue += bwa + 1;    // Base shield and upgrades (same as base weapon)
            unroundValue += sba;        // Speed booster have no base item, only upfrades
            unroundValue++;             // Kinetic Accumulator
            value = unroundValue;

            return calculationReport;
        }
    }
}
