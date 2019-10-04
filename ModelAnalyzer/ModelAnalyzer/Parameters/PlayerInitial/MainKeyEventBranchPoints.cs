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
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float mkebpс = calculator.UpdatedParameter<MainKeyEventBranchPointsCoefficient>().GetValue();
            float ketbp = calculator.UpdatedParameter<KeyEventsTotalBrachPoints>().GetValue();
            float kea = calculator.UpdatedParameter<KeyEventsAmount>().GetValue();

            unroundValue = ketbp / (kea - 1 + mkebpс) * mkebpс;
            value = (float)System.Math.Round(unroundValue, System.MidpointRounding.AwayFromZero);

            return calculationReport;
        }
    }
}
