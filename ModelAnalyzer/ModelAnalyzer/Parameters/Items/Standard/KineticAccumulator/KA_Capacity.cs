using ModelAnalyzer.Services;

namespace ModelAnalyzer.Parameters.Items.Standard.KineticAccumulator
{
    class KA_Capacity : FloatSingleParameter
    {
        public KA_Capacity()
        {
            type = ParameterType.Out;
            title = "Накопитель ТЗ: объем";
            details = "Максимум заряда, который можно собрать в накопителе";
            fractionalDigits = 2;
            tags.Add(ParameterTag.mining);
            tags.Add(ParameterTag.items);
            tags.Add(ParameterTag.baseItems);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float cc = calculator.UpdatedParameter<KA_CapacityCoefficient>().GetValue();
            float pr = calculator.UpdatedParameter<KA_Profit>().GetValue();
            float fp = calculator.UpdatedParameter<KA_FullPrice>().GetValue();

            value = unroundValue = (pr - fp) * cc + fp;

            return calculationReport;
        }
    }
}
