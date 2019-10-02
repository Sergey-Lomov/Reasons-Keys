using System.Linq;

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

            float am = calculator.UpdatedSingleValue(typeof(AverageMining));
            float pa = calculator.UpdatedSingleValue(typeof(PhasesAmount));
            float[] pd = calculator.UpdatedArrayValue(typeof(PhasesDuration));

            if (pd.Length != pa)
            {
                string paTitle = calculator.ParameterTitle(typeof(PhasesAmount));
                string pdTitle = calculator.ParameterTitle(typeof(PhasesDuration));
                string issue = string.Format(invalidPhasesAmount, paTitle, pa, pdTitle, pd.Length);

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
