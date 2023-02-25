using System.Linq;
using System.Collections.Generic;

using ModelAnalyzer.Parameters.Timing;
using ModelAnalyzer.Services;

namespace ModelAnalyzer.Parameters.Mining
{
    class MiningIncrement : FloatSingleParameter
    {
        private readonly string invalidPhasesAmount = "Параметр \"{0}\" = {1}, но длина массива \"{2}\" равна {3}";

        public MiningIncrement()
        {
            type = ParameterType.Inner;
            title = "Прирост добычи";
            details = "Шаг, на который увеличивается добыча ТЗ при увеличение радиуса";
            fractionalDigits = 0;
            tags.Add(ParameterTag.mining);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float am = RequestParameter<AverageMining>(calculator).GetValue();
            float pa = RequestParameter<PhasesAmount>(calculator).GetValue();
            List<float> pd = RequestParameter<PhasesDuration>(calculator).GetValue();

            if (!calculationReport.IsSuccess)
                return calculationReport;

            if (pd.Count != pa)
            {
                string paTitle = RequestParameter<PhasesAmount>(calculator).title;
                string pdTitle = RequestParameter<PhasesDuration>(calculator).title;
                string issue = string.Format(invalidPhasesAmount, paTitle, pa, pdTitle, pd.Count);

                calculationReport.Failed(issue);
                return calculationReport;
            }

            float rightPart = 0;
            for (int n = 0; n < pa; n++)
            {
                float poweredSum = 0;
                float sum = 0;

                for (int i = n; i < pa; i++)
                {
                    poweredSum += i * i;
                    sum += i;
                }

                rightPart += poweredSum / sum * pd[n];
            }

            value = unroundValue = am * pd.Sum() / rightPart;

            return calculationReport;
        }
    }
}
