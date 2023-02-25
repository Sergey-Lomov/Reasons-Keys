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

            float am = RequestParameter<AverageMining>(calculator).GetValue();
            float pr = RequestParameter<KA_Profit>(calculator).GetValue();
            float fp = RequestParameter<KA_FullPrice>(calculator).GetValue();

            value = unroundValue = (pr - fp) / am;

            return calculationReport;
        }
    }
}
