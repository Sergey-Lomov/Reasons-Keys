using ModelAnalyzer.Services;

namespace ModelAnalyzer.Parameters.PlayerInitial
{
    class KeyEventsBranchPoints : FloatSingleParameter
    {
        private const string roundIssueMessage = "Из-за округления реальная сумма очков ветвей на решающих событиях не совпадает со значением параметра\"Кол-во очков ветвей на решающих событиях\"";

        public KeyEventsBranchPoints()
        {
            type = ParameterType.Inner;
            title = "Кол-во очков ветви на не главном решающем событие";
            details = "";
            fractionalDigits = 0;
            tags.Add(ParameterTag.playerInitial);
            tags.Add(ParameterTag.branchPoints);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float mkebp = RequestParmeter<MainKeyEventBranchPoints>(calculator).GetValue();
            float ketbp = RequestParmeter<KeyEventsTotalBrachPoints>(calculator).GetValue();
            float kea = RequestParmeter<KeyEventsAmount>(calculator).GetValue();

            unroundValue = (ketbp - mkebp) / (kea - 1);
            value = (float)System.Math.Round(unroundValue, System.MidpointRounding.AwayFromZero);

            float realTotalPoints = mkebp + value * (kea - 1);
            if (ketbp != realTotalPoints)
                calculationReport.Failed(roundIssueMessage);

            return calculationReport;
        }
    }
}
