using ModelAnalyzer.Services;
using ModelAnalyzer.Parameters.Mining;

namespace ModelAnalyzer.Parameters.Items.Standard.KineticAccumulator
{
    class KA_RelativeRevenue : FloatSingleParameter
    {
        public KA_RelativeRevenue()
        {
            type = ParameterType.Indicator;
            title = "Накопитель ТЗ: относительный доход";
            details = "Показывает как много ЕА может сэкономить на добыче ТЗ игрок, используя накопитель";
            fractionalDigits = 2;
            tags.Add(ParameterTag.items);
            tags.Add(ParameterTag.baseItems);
            tags.Add(ParameterTag.mining);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float am = calculator.UpdatedSingleValue(typeof(AverageMining));
            float pr = calculator.UpdatedSingleValue(typeof(KA_Profit));
            float fp = calculator.UpdatedSingleValue(typeof(KA_FullPrice));

            value = unroundValue = (pr - fp) / am;

            return calculationReport;
        }
    }
}
