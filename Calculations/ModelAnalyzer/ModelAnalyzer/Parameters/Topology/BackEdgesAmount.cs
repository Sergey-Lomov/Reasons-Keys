using ModelAnalyzer.Services;
using ModelAnalyzer.Services.FieldAnalyzer;

namespace ModelAnalyzer.Parameters.Topology
{
    class BackEdgesAmount : FloatSingleParameter
    {
        public BackEdgesAmount()
        {
            type = ParameterType.Inner;
            title = "Кол-во ребер назад";
            details = "Количество всех мест на поле (ребер узлов), в которых могли бы располагаться связи назад.";
            fractionalDigits = 0;
            tags.Add(ParameterTag.topology);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            var abr = calculator.UpdatedParameter<NodesAvailableBackRelations>().GetValue();

            if (!calculationReport.IsSuccess)
                return calculationReport;

            double sumAlpha(float i) => i * abr[(int)i];
            value = unroundValue = (float)MathAdditional.sum(0, Field.nearesNodesAmount, sumAlpha);

            return calculationReport;
        }
    }
}
