using System.Collections.Generic;
using System.Linq;

using ModelAnalyzer.Services;

namespace ModelAnalyzer.Parameters.Items.Artifacts.HoleBox
{
    class HB_MaxTension : FloatSingleParameter
    {
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

            float cpd = RequestParmeter<HB_CollapsePreparationDuration>(calculator).GetValue();
            int mtr = (int)RequestParmeter<HB_MaxTransaction>(calculator).GetValue();
            List<float> tl = RequestParmeter<HB_TensionLimits>(calculator).GetValue();
            List<float> ti = RequestParmeter<HB_TensionIncreasing>(calculator).GetValue();

            if (!calculationReport.IsSuccess)
                return calculationReport;

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
