
using ModelAnalyzer.DataModels;
using ModelAnalyzer.Services;
using ModelAnalyzer.Parameters.Activities;
using ModelAnalyzer.Parameters.PlayerInitial;
using ModelAnalyzer.Parameters.Events;
using System.Collections.Generic;
using System.Linq;

namespace ModelAnalyzer.Parameters.BranchPoints
{
    class BranchPointProfit : FloatSingleParameter
    {
        public BranchPointProfit()
        {
            type = ParameterType.Inner;
            title = "Выгодность очка ветви";
            details = "";
            fractionalDigits = 2;
            tags.Add(ParameterTag.branchPoints);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float eprc = RequestParmeter<EstimatedPrognosedRealisationChance>(calculator).GetValue();
            float ecp = RequestParmeter<EventCreationPrice>(calculator).GetValue();
            var mainDeck = RequestParmeter<MainDeck>(calculator).deck;
            var startDeck = RequestParmeter<StartDeck>(calculator).deck;

            if (!calculationReport.IsSuccess)
                return calculationReport;

            var totalDeck = new List<EventCard>(mainDeck);
            totalDeck.AddRange(startDeck);
            var branchPoints = totalDeck
                .SelectMany(c => c.branchPoints.All())
                .Where(bp => bp.branch == 0 && bp.point > 0)
                .ToList();
            var positiveCards = branchPoints.Count;
            var positivePoints = branchPoints.Select(bp => bp.point).Sum();

            value = unroundValue = positiveCards / (positivePoints * eprc) * ecp;

            return calculationReport;
        }
    }
}
