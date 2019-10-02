using System.Linq;

using ModelAnalyzer.Services;

namespace ModelAnalyzer.Parameters.Items.Standard.BaseWeapon
{
    class BW_Profit : FloatSingleParameter
    {
        public BW_Profit()
        {
            type = ParameterType.Inner;
            title = "БО: выгодность";
            details = "Усредненная выгодность БО с различным кол-вом улучшений";
            fractionalDigits = 2;
            tags.Add(ParameterTag.items);
            tags.Add(ParameterTag.baseItems);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float[] up = calculator.UpdatedArrayValue(typeof(BW_UpgradesProfit));
            if (up.Count() > 0)
                value = unroundValue = up.Average();

            return calculationReport;
        }
    }
}
