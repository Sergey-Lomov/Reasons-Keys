using System.Linq;
using ModelAnalyzer.Parameters.General;
using ModelAnalyzer.Services;

namespace ModelAnalyzer.Parameters.Events
{
    class BranchPointsDisbalance : FloatSingleParameter
    {
        private const float criticalValue = 0.05f;
        private const string bigValueIssue = "Отклонение слишком велико, нужно увеличить параметр \"Кол-во итераций при балансировке очков ветвей\"";

        public BranchPointsDisbalance()
        {
            type = ParameterType.Indicator;
            title = "Дисбаланс очков ветвей";
            details = "Этот параметр отражает то, насколько самой легкой из ветвей легче выиграть, чем самой тяжелой";
            fractionalDigits = 2;
            tags.Add(ParameterTag.events);
            tags.Add(ParameterTag.branchPoints);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            var aripc = RequestParmeter<AverageRelationsImpactPerCount>(calculator).GetValue();
            var maxpa = (int)RequestParmeter<MaxPlayersAmount>(calculator).GetValue();
            var deck = RequestParmeter<MainDeck>(calculator).deck;

            if (!calculationReport.IsSuccess)
                return calculationReport;

            var stabilities = EventCardsAnalizer.CardsStabilities(deck, aripc);
            value = unroundValue = EventCardsAnalizer.BrancheDisbalance(deck, stabilities, maxpa);

            return calculationReport;
        }

        internal override ParameterValidationReport Validate(Validator validator, Storage storage)
        {
            var report = base.Validate(validator, storage);

            if (unroundValue > criticalValue)
                report.AddIssue(bigValueIssue);

            return report;
        }
    }
}
