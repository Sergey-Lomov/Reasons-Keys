using ModelAnalyzer.Parameters.Mining;

namespace ModelAnalyzer.Parameters.Moving
{
    class MotionPrice : SingleParameter
    {
        public MotionPrice()
        {
            type = ParameterType.Out;
            title = "Полная цена движения";
            details = "На сейчас она же - цена движения в ТЗ";
            fractionalDigits = 0;
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float ad = calculator.GetUpdateSingleValue(typeof(AverageDistance));
            float am = calculator.GetUpdateSingleValue(typeof(AverageMining));
            float mfl = calculator.GetUpdateSingleValue(typeof(MotionFreeLevel));

            unroundValue = am / mfl / ad;
            value = (float) System.Math.Round(unroundValue);

            return calculationReport;
        }
    }
}
