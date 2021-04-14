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

            float cna = RequestParmeter<ContinuumNodesAmount>(calculator).GetValue();
            float eca = RequestParmeter<EventCreationAmount>(calculator).GetValue();
            float eifp = RequestParmeter<EventImpactPrice>(calculator).GetValue();
            float ua = RequestParmeter<SS_UsageAmount>(calculator).GetValue();
            float sna = RequestParmeter<SS_SymmetricalNodesAmount>(calculator).GetValue();
            float sp = RequestParmeter<SS_ImpactPower>(calculator).GetValue();
            float ssp = RequestParmeter<SS_SecondaryImpactPower>(calculator).GetValue();

            value = unroundValue = (sp + (eca / cna * sna * ssp)) * eifp * ua;

            return calculationReport;
        }
    }
}
