using System.Collections.Generic;
using System.Linq;

using ModelAnalyzer.Services;
using ModelAnalyzer.Parameters.PlayerInitial;
using ModelAnalyzer.Parameters.General;

namespace ModelAnalyzer.Parameters.Activities
{
    class AverageEventsConcreteBranchPoints : FloatSingleParameter
    {
        readonly int[] BranchPointsAmounts = {2, 2, 1, 1, 1, 1, 0};

        public AverageEventsConcreteBranchPoints()
        {
            type = ParameterType.Inner;
            title = "Среднее кол-во очков конкретной ветви на разыгранных событиях";
            details = "Имеется ввиду среднее кол-во очков (как положительных, так и отрицательных) одной конкретной ветви на всех событиях, разыгранных за партию. Не зависит от кол-ва игроков - усреднено.";
            fractionalDigits = 2;
            tags.Add(ParameterTag.activities);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float minpa = calculator.UpdatedParameter<MinPlayersAmount>().GetValue();
            float maxpa = calculator.UpdatedParameter<MaxPlayersAmount>().GetValue();
            float kebp = calculator.UpdatedParameter<KeyEventsTotalBrachPoints>().GetValue();
            float kea = calculator.UpdatedParameter<KeyEventsAmount>().GetValue();
            float keca = calculator.UpdatedParameter<KeyEventCreationAmount>().GetValue();
            List<float> auecbp = calculator.UpdatedParameter<AverageUnkeyEventsConcreteBranchPoints>().GetValue();

            value = unroundValue = auecbp.Sum() / (maxpa - minpa + 1) + kebp * keca / kea;

            return calculationReport;
        }
    }
}
