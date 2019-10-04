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

            float bspr = calculator.UpdatedParameter<BS_Profit>().GetValue();
            float bwpr = calculator.UpdatedParameter<BW_Profit>().GetValue();
            float kapr = calculator.UpdatedParameter<KA_Profit>().GetValue();
            float sbpr = calculator.UpdatedParameter<SB_Profit>().GetValue();

            value = unroundValue = (bspr + bwpr + kapr +sbpr) / 4;

            return calculationReport;
        }
    }
}
