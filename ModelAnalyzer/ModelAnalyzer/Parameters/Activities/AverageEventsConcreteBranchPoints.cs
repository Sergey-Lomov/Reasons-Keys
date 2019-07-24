using System;
using System.Collections.Generic;
using System.Linq;

using ModelAnalyzer.Parameters.PlayerInitial;

namespace ModelAnalyzer.Parameters.Activities
{
    class AverageEventsConcreteBranchPoints : SingleParameter
    {
        readonly int[] BranchPointsAmounts = {2, 2, 1, 1, 1, 1, 0};

        public AverageEventsConcreteBranchPoints()
        {
            type = ParameterType.Inner;
            title = "Среднее кол-во очков конкретной ветви на разыгранных событиях";
            details = "Имеется ввиду среднее кол-во очков (как положительных, так и отрицательных) одной конкретной ветви на всех событиях, разыгранных за партию. Не зависит от кол-ва игроков - усреднено.";
            fractionalDigits = 2;
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float minpa = calculator.UpdateSingleValue(typeof(MinPlayersAmount));
            float maxpa = calculator.UpdateSingleValue(typeof(MaxPlayersAmount));
            float kebp = calculator.UpdateSingleValue(typeof(KeyEventsTotalBrachPoints));
            float kea = calculator.UpdateSingleValue(typeof(KeyEventsAmount));
            float keca = calculator.UpdateSingleValue(typeof(KeyEventCreationAmount));
            float[] auecbp = calculator.UpdateArrayValue(typeof(AverageUnkeyEventsConcreteBranchPoints));
            
            value = unroundValue = auecbp.Sum() / (maxpa - minpa + 1) + kebp * keca / kea;

            return calculationReport;
        }
    }
}
