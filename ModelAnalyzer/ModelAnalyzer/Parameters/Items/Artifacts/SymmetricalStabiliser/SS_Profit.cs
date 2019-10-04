using ModelAnalyzer.Services;
using ModelAnalyzer.Parameters.Topology;
using ModelAnalyzer.Parameters.Activities;

namespace ModelAnalyzer.Parameters.Items.Artifacts.SymmetricalStabiliser
{
    class SS_Profit : ArtifactProfit
    {
        public SS_Profit()
        {
            type = ParameterType.Inner;
            title = "СС: Выгодность";
            details = "";
            fractionalDigits = 2;
            tags.Add(ParameterTag.items);
            tags.Add(ParameterTag.artifacts);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float cna = calculator.UpdatedParameter<ContinuumNodesAmount>().GetValue();
            float eca = calculator.UpdatedParameter<EventCreationAmount>().GetValue();
            float eifp = calculator.UpdatedParameter<EventImpactPrice>().GetValue();
            float ua = calculator.UpdatedParameter<SS_UsageAmount>().GetValue();
            float sna = calculator.UpdatedParameter<SS_SymmetricalNodesAmount>().GetValue();
            float sp = calculator.UpdatedParameter<SS_StabilisationPower>().GetValue();
            float ssp = calculator.UpdatedParameter<SS_SecondaryStabilisationPower>().GetValue();

            value = unroundValue = (sp + (eca / cna * sna * ssp)) * eifp * ua;

            return calculationReport;
        }
    }
}
