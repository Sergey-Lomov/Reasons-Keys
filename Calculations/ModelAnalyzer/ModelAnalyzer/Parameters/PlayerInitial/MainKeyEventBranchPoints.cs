using ModelAnalyzer.Services;

namespace ModelAnalyzer.Parameters.PlayerInitial
{
    class MainKeyEventBranchPoints : FloatSingleParameter
    {
        public MainKeyEventBranchPoints()
        {
            type = ParameterType.Inner;
            title = "Кол-во очков ветви на главном решающем событие";
            details = "";
            fractionalDigits = 0;
            tags.Add(ParameterTag.playerInitial);
            tags.Add(ParameterTag.branchPoints);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float mkebpс = RequestParameter<MainKeyEventBranchPointsCoefficient>(calculator).GetValue();
            float ketbp = RequestParameter<KeyEventsTotalBrachPoints>(calculator).GetValue();
            float kea = RequestParameter<KeyEventsAmount>(calculator).GetValue();

            unroundValue = ketbp / (kea - 1 + mkebpс) * mkebpс;
            value = (float)System.Math.Round(unroundValue, System.MidpointRounding.AwayFromZero);

            return calculationReport;
        }
    }
}
