namespace ModelAnalyzer.Parameters.Items.Standard.KineticAccumulator
{
    class KA_Capacity : SingleParameter
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

            float cc = calculator.UpdatedSingleValue(typeof(KA_CapacityCoefficient));
            float pr = calculator.UpdatedSingleValue(typeof(KA_Profit));
            float fp = calculator.UpdatedSingleValue(typeof(KA_FullPrice));

            value = unroundValue = (pr - fp) * cc + fp;

            return calculationReport;
        }
    }
}
