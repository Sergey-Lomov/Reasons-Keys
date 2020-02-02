using ModelAnalyzer.Services;

namespace ModelAnalyzer.Parameters.Items.Artifacts.SymmetricalStabiliser
{
    class SS_SymmetricalNodesAmount : FloatSingleParameter
    {
        public SS_SymmetricalNodesAmount()
        {
            type = ParameterType.Out;
            title = "СС: кол-во симметричных узлов";
            details = "Кол-во узлов, которые считаются симметричными к тому, на который направлено воздействие СС";
            fractionalDigits = 0;
            tags.Add(ParameterTag.items);
            tags.Add(ParameterTag.artifacts);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            var cm = calculator.UpdatedModule<SS_CalculationModule>();
            value = unroundValue = cm.symmetricalNodesAmount;

            return calculationReport;
        }
    }
}
