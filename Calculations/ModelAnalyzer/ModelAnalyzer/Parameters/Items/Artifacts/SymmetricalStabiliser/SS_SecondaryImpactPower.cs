using ModelAnalyzer.Services;

namespace ModelAnalyzer.Parameters.Items.Artifacts.SymmetricalStabiliser
{
    class SS_SecondaryImpactPower : FloatSingleParameter
    {
        public SS_SecondaryImpactPower()
        {
            type = ParameterType.Out;
            title = "СС: сила вторичного воздействия";
            details = "Сила, с которой СС воздействует на узлы, симметричных к тому, на который направлено основное воздействие";
            fractionalDigits = 0;
            tags.Add(ParameterTag.items);
            tags.Add(ParameterTag.artifacts);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            var cm = RequestModule<SS_CalculationModule>(calculator);
            value = unroundValue = cm.secondaryImpactPower;

            return calculationReport;
        }
    }
}
