namespace ModelAnalyzer.Parameters.Items
{
    class AverageBaseItemsProfit : SingleParameter
    {
        public AverageBaseItemsProfit()
        {
            type = ParameterType.Inner;
            title = "Средняя выгодность базовых предметов";
            details = "Среднее арифметическое выгодностей всех базовых предметов";
            fractionalDigits = 2;
            tags.Add(ParameterTag.baseItems);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            //float p = calculator.UpdateSingleValue(typeof(ParamName));

            value = 0;

            return calculationReport;
        }
    }
}
