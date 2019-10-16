using ModelAnalyzer.Services;
using ModelAnalyzer.Parameters.Items.Standard.BaseShield;
using ModelAnalyzer.Parameters.Items.Standard.BaseWeapon;
using ModelAnalyzer.Parameters.Items.Standard.KineticAccumulator;
using ModelAnalyzer.Parameters.Items.Standard.SpeedBooster;

namespace ModelAnalyzer.Parameters.Items
{
    class AverageBaseItemsProfit : FloatSingleParameter
    {
        public AverageBaseItemsProfit()
        {
            type = ParameterType.Inner;
            title = "Средняя выгодность базовых предметов";
            details = "Среднее арифметическое выгодностей всех базовых предметов";
            fractionalDigits = 2;
            tags.Add(ParameterTag.baseItems);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float bspr = RequestParmeter<BS_Profit>(calculator).GetValue();
            float bwpr = RequestParmeter<BW_Profit>(calculator).GetValue();
            float kapr = RequestParmeter<KA_Profit>(calculator).GetValue();
            float sbpr = RequestParmeter<SB_Profit>(calculator).GetValue();

            value = unroundValue = (bspr + bwpr + kapr +sbpr) / 4;

            return calculationReport;
        }
    }
}
