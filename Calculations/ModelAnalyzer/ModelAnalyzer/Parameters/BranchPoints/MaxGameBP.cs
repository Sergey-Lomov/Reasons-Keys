using System.Linq;

using ModelAnalyzer.Services;
using ModelAnalyzer.Parameters.General;
using ModelAnalyzer.Parameters.Events;
using ModelAnalyzer.Parameters.PlayerInitial;
using ModelAnalyzer.DataModels;
using System;

namespace ModelAnalyzer.Parameters.BranchPoints
{
    class MaxGameBP : FloatSingleParameter
    {
        public MaxGameBP()
        {
            type = ParameterType.Inner;
            title = "Максимальное кол-во очков ветви за партию";
            details = "";
            fractionalDigits = 0;
            tags.Add(ParameterTag.branchPoints);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            var maxpa = RequestParameter<MaxPlayersAmount>(calculator).GetValue();
            var ketbp = RequestParameter<KeyEventsTotalBrachPoints>(calculator).GetValue();
            var mainDeck = RequestParameter<MainDeck>(calculator);

            if (!calculationReport.IsSuccess)
                return calculationReport;

            var positiveBp = new BranchPoint(0, 1);
            int cpea = mainDeck.deck.Where(c => c.branchPoints.ContainsBranchPoint(positiveBp)).Count();

            value = unroundValue = maxpa - 1 + ketbp + cpea;

            return calculationReport;
        }
    }
}
