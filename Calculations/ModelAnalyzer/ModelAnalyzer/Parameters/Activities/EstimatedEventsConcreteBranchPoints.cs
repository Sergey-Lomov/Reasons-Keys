using System.Collections.Generic;
using System.Linq;

using ModelAnalyzer.Services;
using ModelAnalyzer.Parameters.PlayerInitial;
using ModelAnalyzer.Parameters.General;

namespace ModelAnalyzer.Parameters.Activities
{
    class EstimatedEventsConcreteBranchPoints : FloatSingleParameter
    {
        public EstimatedEventsConcreteBranchPoints()
        {
            type = ParameterType.Indicator;
            title = "Расчетное кол-во очков конкретной ветви на разыгранных событиях";
            details = "Имеется ввиду среднее кол-во очков (как положительных, так и отрицательных) одной конкретной ветви на всех событиях, разыгранных за партию. Не зависит от кол-ва игроков - усреднено.";
            fractionalDigits = 2;
            tags.Add(ParameterTag.branchPoints);
            tags.Add(ParameterTag.activities);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float minpa = RequestParmeter<MinPlayersAmount>(calculator).GetValue();
            float maxpa = RequestParmeter<MaxPlayersAmount>(calculator).GetValue();
            float kebp = RequestParmeter<KeyEventsTotalBrachPoints>(calculator).GetValue();
            float kea = RequestParmeter<KeyEventsAmount>(calculator).GetValue();
            float keca = RequestParmeter<KeyEventCreationAmount>(calculator).GetValue();
            List<float> auecbp = RequestParmeter<AverageUnkeyEventsConcreteBranchPoints>(calculator).GetValue();

            if (!calculationReport.IsSuccess)
                return calculationReport;

            value = unroundValue = auecbp.Sum() / (maxpa - minpa + 1) + kebp * keca / kea;

            return calculationReport;
        }
    }
}
