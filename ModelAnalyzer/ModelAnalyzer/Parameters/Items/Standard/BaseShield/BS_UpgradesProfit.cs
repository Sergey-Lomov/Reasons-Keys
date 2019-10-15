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

            float saa = calculator.UpdatedParameter<AtackAmount>().GetValue();
            List<float> bsd = calculator.UpdatedParameter<BS_Defense>().GetValue();

            unroundValues.Clear();
            values.Clear();

            foreach (var defense in bsd)
                unroundValues.Add(saa * defense);

            values = unroundValues;

            return calculationReport;
        }
    }
}
