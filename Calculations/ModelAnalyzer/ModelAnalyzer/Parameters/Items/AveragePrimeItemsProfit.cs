using ModelAnalyzer.Services;
using ModelAnalyzer.Parameters.Items.Standard.BaseShield;
using ModelAnalyzer.Parameters.Items.Standard.BaseWeapon;
using ModelAnalyzer.Parameters.Items.Standard.KineticAccumulator;
using ModelAnalyzer.Parameters.Items.Standard.SpeedBooster;

namespace ModelAnalyzer.Parameters.Items
{
    class AveragePrimeItemsProfit : FloatSingleParameter
    {
        public AveragePrimeItemsProfit()
        {
            type = ParameterType.Inner;
            title = "Средняя выгодность первичных предметов";
            details = "Среднее арифметическое выгодностей всех первичных предметов";
            fractionalDigits = 2;
            tags.Add(ParameterTag.items);
            tags.Add(ParameterTag.baseItems);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float bspr = RequestParameter<BS_Profit>(calculator).GetValue();
            float bwpr = RequestParameter<BW_Profit>(calculator).GetValue();
            float kapr = RequestParameter<KA_Profit>(calculator).GetValue();
            float sbpr = RequestParameter<SB_Profit>(calculator).GetValue();

            value = unroundValue = (bspr + bwpr + kapr + sbpr) / 4;

            return calculationReport;
        }
    }
}
