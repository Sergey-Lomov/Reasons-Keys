using ModelAnalyzer.Services;

namespace ModelAnalyzer.Parameters.Items.Artifacts.SymmetricalStabiliser
{
    class SS_ImpactPower : FloatSingleParameter
    {
        public SS_ImpactPower()
        {
            type = ParameterType.Out;
            title = "СС: сила воздействия";
            details = "Сила, с которой СС оказывает воздействие на узел, в котором находится игрок";
            fractionalDigits = 0;
            tags.Add(ParameterTag.items);
            tags.Add(ParameterTag.artifacts);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            var cm = calculator.UpdatedModule<SS_CalculationModule>();
            value = unroundValue = cm.impactPower;

            return calculationReport;
        }
    }
}
