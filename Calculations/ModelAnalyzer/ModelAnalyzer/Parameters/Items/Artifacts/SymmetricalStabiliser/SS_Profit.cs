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
            title = "СС: выгодность";
            details = "";
            fractionalDigits = 2;
            tags.Add(ParameterTag.items);
            tags.Add(ParameterTag.artifacts);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float cna = RequestParameter<ContinuumNodesAmount>(calculator).GetValue();
            float eca = RequestParameter<EventCreationAmount>(calculator).GetValue();
            float eifp = RequestParameter<EventImpactPrice>(calculator).GetValue();
            float ua = RequestParameter<SS_UsageAmount>(calculator).GetValue();
            float sna = RequestParameter<SS_SymmetricalNodesAmount>(calculator).GetValue();
            float sp = RequestParameter<SS_ImpactPower>(calculator).GetValue();
            float ssp = RequestParameter<SS_SecondaryImpactPower>(calculator).GetValue();

            value = unroundValue = (sp + (eca / cna * sna * ssp)) * eifp * ua;

            return calculationReport;
        }
    }
}
