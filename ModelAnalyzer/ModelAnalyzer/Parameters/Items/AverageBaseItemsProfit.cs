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

            float bspr = calculator.UpdatedSingleValue(typeof(BS_Profit));
            float bwpr = calculator.UpdatedSingleValue(typeof(BW_Profit));
            float kapr = calculator.UpdatedSingleValue(typeof(KA_Profit));
            float sbpr = calculator.UpdatedSingleValue(typeof(SB_Profit));

            value = unroundValue = (bspr + bwpr + kapr +sbpr) / 4;

            return calculationReport;
        }
    }
}
