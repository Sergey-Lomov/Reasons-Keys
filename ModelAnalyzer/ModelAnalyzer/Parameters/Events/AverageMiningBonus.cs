using System.Linq;

using ModelAnalyzer.Services;

namespace ModelAnalyzer.Parameters.Events
{
    class AverageMiningBonus : FloatSingleParameter
    {
        public AverageMiningBonus()
        {
            type = ParameterType.Inner;
            title = "Средний бонус добычи ТЗ";
            details = "Среднее арифметическое бонуса добычи на всех картах конитнуума";
            fractionalDigits = 2;
            tags.Add(ParameterTag.events);
            tags.Add(ParameterTag.mining);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float cna = calculator.UpdatedSingleValue(typeof(ContinuumNodesAmount));
            float[] mba = calculator.UpdatedArrayValue(typeof(EventMiningBonusAllocation));

            float average = 0;
            for (int i = 0; i < mba.Count(); i++)
                average += i * mba[i] / mba.Sum();

            value = unroundValue = average;

            return calculationReport;
        }
    }
}
