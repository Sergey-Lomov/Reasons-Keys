namespace ModelAnalyzer.Parameters.PlayerInitial
{
    class KeyEventsBranchPoints : SingleParameter
    {
        private const string roundIssueMessage = "Из-за округления реальная сумма очков ветвей на решающих событиях не совпадает со значением параметра\"Кол-во очков ветвей на решающих событиях\"";

        public KeyEventsBranchPoints()
        {
            type = ParameterType.Inner;
            title = "Кол-во очков ветви на не главном решающем событие";
            details = "";
            fractionalDigits = 0;
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float mkebp = calculator.UpdatedSingleValue(typeof(MainKeyEventBranchPoints));
            float ketbp = calculator.UpdatedSingleValue(typeof(KeyEventsTotalBrachPoints));
            float kea = calculator.UpdatedSingleValue(typeof(KeyEventsAmount));

            unroundValue = (ketbp - mkebp) / (kea - 1);
            value = (float)System.Math.Round(unroundValue, System.MidpointRounding.AwayFromZero);

            float realTotalPoints = mkebp + value * (kea - 1);
            if (ketbp != realTotalPoints)
                calculationReport.Failed(roundIssueMessage);

            return calculationReport;
        }
    }
}
