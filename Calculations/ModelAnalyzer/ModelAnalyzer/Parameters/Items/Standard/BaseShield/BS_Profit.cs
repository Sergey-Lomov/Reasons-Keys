using System.Linq;
using System.Collections.Generic;

using ModelAnalyzer.Services;

namespace ModelAnalyzer.Parameters.Items.Standard.BaseShield
{
    class BS_Profit : FloatSingleParameter
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

            List<float> up = RequestParameter<BS_UpgradesProfit>(calculator).GetValue();

            if (!calculationReport.IsSuccess)
                return calculationReport;

            if (up.Count() > 0)
                value = unroundValue = up.Average();

            return calculationReport;
        }
    }
}
