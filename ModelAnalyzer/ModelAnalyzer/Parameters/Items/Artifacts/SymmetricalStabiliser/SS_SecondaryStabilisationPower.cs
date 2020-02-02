using ModelAnalyzer.Services;

namespace ModelAnalyzer.Parameters.Items.Artifacts.SymmetricalStabiliser
{
    class SS_SecondaryStabilisationPower : FloatSingleParameter
    {
        public SS_SecondaryStabilisationPower()
        {
            type = ParameterType.Out;
            title = "СС: сила вторичной стабилизации";
            details = "Сила, с которой СС влияет на стабильность узлов, симметричных к тому, на который направлено основное воздействие";
            fractionalDigits = 0;
            tags.Add(ParameterTag.items);
            tags.Add(ParameterTag.artifacts);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            var cm = calculator.UpdatedModule<SS_CalculationModule>();
            value = unroundValue = cm.secondaryStabilisationPower;

            return calculationReport;
        }
    }
}
