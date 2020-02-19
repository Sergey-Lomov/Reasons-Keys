using System.Linq;

using ModelAnalyzer.Services;
using ModelAnalyzer.Parameters.General;
using ModelAnalyzer.Parameters.Events;
using ModelAnalyzer.Parameters.PlayerInitial;
using ModelAnalyzer.DataModels;
using System;

namespace ModelAnalyzer.Parameters.BranchPointsTrack
{
    class MaxGameBP : FloatSingleParameter
    {
        public MaxGameBP()
        {
            type = ParameterType.Inner;
            title = "Максимальное кол-во очков ветви за партию";
            details = "";
            fractionalDigits = 0;
            tags.Add(ParameterTag.branchPointsTrack);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float maxpa = RequestParmeter<MaxPlayersAmount>(calculator).GetValue();
            float ketbp = RequestParmeter<KeyEventsTotalBrachPoints>(calculator).GetValue();
            DeckParameter mainDeck = RequestParmeter<MainDeck>(calculator);

            var positiveBp = new BranchPoint(0, 1);
            int cpea = mainDeck.deck.Where(c => c.branchPoints.ContainsBranchPoint(positiveBp)).Count();

            if (!calculationReport.IsSuccess)
                return calculationReport;

            value = unroundValue = maxpa - 1 + ketbp + cpea;

            return calculationReport;
        }
    }
}
