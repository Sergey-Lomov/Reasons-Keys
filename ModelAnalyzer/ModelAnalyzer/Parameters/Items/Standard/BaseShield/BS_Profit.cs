using System.Linq;

namespace ModelAnalyzer.Parameters.Items.Standard.BaseShield
{
    class BS_Profit : SingleParameter
    {
        public BS_Profit()
        {
            type = ParameterType.Inner;
            title = "БЩ: выгодность";
            details = "Усредненная выгодность щита с различным кол-вом улучшений";
            fractionalDigits = 2;
            tags.Add(ParameterTag.items);
            tags.Add(ParameterTag.baseItems);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float[] up = calculator.UpdatedArrayValue(typeof(BS_UpgradesProfit));
            if (up.Count() > 0)
                value = unroundValue = up.Average();

            return calculationReport;
        }
    }
}
