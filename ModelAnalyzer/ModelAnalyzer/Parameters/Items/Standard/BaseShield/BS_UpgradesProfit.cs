using System.Collections.Generic;

using ModelAnalyzer.Services;
using ModelAnalyzer.Parameters.Activities;

namespace ModelAnalyzer.Parameters.Items.Standard.BaseShield
{
    class BS_UpgradesProfit : FloatArrayParameter
    {
        public BS_UpgradesProfit()
        {
            type = ParameterType.Inner;
            title = "БЩ: выгодность улучшений";
            details = "Выгодность БЩ при различном кол-ве улучшений (включая 0 улучшений)";
            fractionalDigits = 2;
            tags.Add(ParameterTag.items);
            tags.Add(ParameterTag.baseItems);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float saa = RequestParmeter<AtackAmount>(calculator).GetValue();
            List<float> bsd = RequestParmeter<BS_Defense>(calculator).GetValue();

            if (!calculationReport.IsSuccess)
                return calculationReport;

            unroundValues = new List<float>();
            values = new List<float>();

            foreach (var defense in bsd)
                unroundValues.Add(saa * defense);

            values = unroundValues;

            return calculationReport;
        }
    }
}
