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

            float mkebpс = calculator.UpdatedSingleValue(typeof(MainKeyEventBranchPointsCoefficient));
            float ketbp = calculator.UpdatedSingleValue(typeof(KeyEventsTotalBrachPoints));
            float kea = calculator.UpdatedSingleValue(typeof(KeyEventsAmount));

            unroundValue = ketbp / (kea - 1 + mkebpс) * mkebpс;
            value = (float)System.Math.Round(unroundValue, System.MidpointRounding.AwayFromZero);

            return calculationReport;
        }
    }
}
