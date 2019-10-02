using System.Linq;

using ModelAnalyzer.Services;

namespace ModelAnalyzer.Parameters.Items.Artifacts.HoleBox
{
    class HB_MaxTension : FloatSingleParameter
    {
        private const string emptyArrayIssue = "Параметр {0} не содержит значений";

        public HB_MaxTension()
        {
            type = ParameterType.Out;
            title = "ДК: максимальная напряженность";
            details = "Максимальная напряженность, при достижении которой артефакт коллапсирует";
            fractionalDigits = 0;
            tags.Add(ParameterTag.items);
            tags.Add(ParameterTag.artifacts);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float cpd = calculator.UpdatedSingleValue(typeof(HB_CollapsePreparationDuration));
            int mtr = (int)calculator.UpdatedSingleValue(typeof(HB_MaxTransaction));
            float[] tl = calculator.UpdatedArrayValue(typeof(HB_TensionLimits));
            float[] ti = calculator.UpdatedArrayValue(typeof(HB_TensionIncreasing));

            if (tl.Count() == 0)
            {
                var title = calculator.ParameterTitle(typeof(HB_TensionLimits));
                var mesasge = string.Format(emptyArrayIssue, title);
                calculationReport.AddFailed(mesasge);
            }

            if (ti.Count() == 0)
            {
                var title = calculator.ParameterTitle(typeof(HB_TensionIncreasing));
                var mesasge = string.Format(emptyArrayIssue, title);
                calculationReport.AddFailed(mesasge);
            }

            if (!calculationReport.IsSucces)
            {
                value = unroundValue = 0;
                return calculationReport;
            }


            int maeu = (int)tl.Last();

            int antiEU = 0;
            int tension = 0;
            for (int i = 0; i < cpd - 1; i++)
            {
                antiEU = antiEU + mtr < maeu ? antiEU + mtr : maeu;
                int increasingStep = 0;
                while (antiEU > tl[increasingStep])
                    increasingStep++;
                tension += (int)ti[increasingStep];
            }

            value = unroundValue = tension + 1;

            return calculationReport;
        }
    }
}
