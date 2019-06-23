using System.Linq;

using ModelAnalyzer.Parameters.Timing;

namespace ModelAnalyzer.Parameters.Mining
{
    class MiningIncrement : SingleParameter
    {
        private readonly string invalidPhasesAmount = "Параметр \"{0}\" = {1}, но лдина массива \"{2}\" равна {3}";

        public MiningIncrement()
        {
            type = ParameterType.Inner;
            title = "Прирост добычи";
            details = "Шаг, на который увеличивается добыча ТЗ при увеличение радиуса";
            fractionalDigits = 0;
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float am = calculator.UpdateSingleValue(typeof(AverageMining));
            float pa = calculator.UpdateSingleValue(typeof(PhasesAmount));
            float[] pd = calculator.UpdateArrayValue(typeof(PhasesDuration));

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
