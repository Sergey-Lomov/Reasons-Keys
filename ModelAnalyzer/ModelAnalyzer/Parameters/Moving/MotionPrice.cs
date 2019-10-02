using ModelAnalyzer.Services;
using ModelAnalyzer.Parameters.Mining;
using ModelAnalyzer.Parameters.Topology;

namespace ModelAnalyzer.Parameters.Moving
{
    class MotionPrice : FloatSingleParameter
    {
        public MotionPrice()
        {
            type = ParameterType.Out;
            title = "Стоимость перемещения (ТЗ)";
            details = "Перемещение всегда требует только ТЗ. Поэтому стоимость перемещения (ТЗ) является также и полной стоимостью перемещения.";
            fractionalDigits = 0;
            tags.Add(ParameterTag.moving);
            tags.Add(ParameterTag.activities);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float ad = calculator.UpdatedSingleValue(typeof(AverageDistance));
            float am = calculator.UpdatedSingleValue(typeof(AverageMining));
            float mfl = calculator.UpdatedSingleValue(typeof(MotionFreeLevel));

            unroundValue = am / mfl / ad;
            value = (float) System.Math.Round(unroundValue);

            return calculationReport;
        }
    }
}
