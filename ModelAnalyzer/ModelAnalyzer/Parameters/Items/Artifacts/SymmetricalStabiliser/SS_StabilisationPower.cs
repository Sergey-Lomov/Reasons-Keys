using ModelAnalyzer.Services;

namespace ModelAnalyzer.Parameters.Items.Artifacts.SymmetricalStabiliser
{
    class SS_StabilisationPower : FloatSingleParameter
    {
        public SS_StabilisationPower()
        {
            type = ParameterType.Out;
            title = "СС: сила стабилизации";
            details = "Сила, с которой СС влияет на стабильность выбраного владельцем узла";
            fractionalDigits = 0;
            tags.Add(ParameterTag.items);
            tags.Add(ParameterTag.artifacts);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            var cm = calculator.UpdatedModule<SS_CalculationModule>();
            value = unroundValue = cm.stabilisationPower;

            return calculationReport;
        }
    }
}
