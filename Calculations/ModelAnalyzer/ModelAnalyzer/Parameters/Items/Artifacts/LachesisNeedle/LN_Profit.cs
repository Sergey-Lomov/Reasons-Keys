using ModelAnalyzer.Services;

namespace ModelAnalyzer.Parameters.Items.Artifacts.LachesisNeedle
{
    class LN_Profit : ArtifactProfit
    {
        public LN_Profit()
        {
            type = ParameterType.Inner;
            title = "ИЛ: выгодность";
            details = "";
            fractionalDigits = 2;
            tags.Add(ParameterTag.items);
            tags.Add(ParameterTag.artifacts);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float ca = RequestParameter<LN_ConnectionsAmount>(calculator).GetValue();
            float ocpr = RequestParameter<LN_OneConnectionProfit>(calculator).GetValue();

            value = unroundValue = ca * ocpr;

            return calculationReport;
        }
    }
}
