using ModelAnalyzer.Services;
using ModelAnalyzer.Services.FieldAnalyzer;

namespace ModelAnalyzer.Parameters.Topology
{
    class ContinuumNodesAmount : FloatSingleParameter
    {
        const int topologicIncrement = Field.nearesNodesAmount;

        public ContinuumNodesAmount()
        {
            type = ParameterType.Inner;
            title = "Кол-во узлов континуума";
            details = "";
            fractionalDigits = 0;
            tags.Add(ParameterTag.events);
            tags.Add(ParameterTag.topology);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float fr = RequestParmeter<FieldRadius>(calculator).GetValue();

            value = 0;
            for (int i = 1; i <= fr; i++)
                value += i * topologicIncrement;
            unroundValue = value;

            return calculationReport;
        }
    }
}
