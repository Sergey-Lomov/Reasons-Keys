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

            float cc = RequestParameter<KA_CapacityCoefficient>(calculator).GetValue();
            float pr = RequestParameter<KA_Profit>(calculator).GetValue();
            float fp = RequestParameter<KA_FullPrice>(calculator).GetValue();

            value = unroundValue = (pr - fp) * cc + fp;

            return calculationReport;
        }
    }
}
