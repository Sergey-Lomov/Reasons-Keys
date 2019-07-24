namespace ModelAnalyzer.Parameters.Events
{
    class ContinuumNodesAmount : SingleParameter
    {
        const int topologicIncrement = 6;

        public ContinuumNodesAmount()
        {
            type = ParameterType.Inner;
            title = "Кол-во узлов континуума";
            details = "";
            fractionalDigits = 0;
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float fr = calculator.UpdatedSingleValue(typeof(FieldRadius));

            value = 0;
            for (int i = 1; i <= fr; i++)
                value += i * topologicIncrement;
            unroundValue = value;

            return calculationReport;
        }
    }
}
