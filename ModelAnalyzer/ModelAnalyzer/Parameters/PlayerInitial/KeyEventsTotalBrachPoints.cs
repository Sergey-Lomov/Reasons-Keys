using System;
using System.Linq;

using ModelAnalyzer.Parameters.Activities;

namespace ModelAnalyzer.Parameters.PlayerInitial
{
    class KeyEventsTotalBrachPoints : SingleParameter
    {
        public KeyEventsTotalBrachPoints()
        {
            type = ParameterType.Out;
            title = "Кол-во очков ветвей на решающих событиях";
            details = "Кол-во очков ветвей на всех решающих картах одного игрока.";
            fractionalDigits = 0;
            tags.Add(ParameterTag.playerInitial);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float kebpc = calculator.UpdatedSingleValue(typeof(KeyEventsBranchPointsCoefficient));
            float[] auecbp = calculator.UpdatedArrayValue(typeof(AverageUnkeyEventsConcreteBranchPoints));

            unroundValue = auecbp.Sum() / auecbp.Length * kebpc;
            value = (float)Math.Round(unroundValue, MidpointRounding.AwayFromZero);

            return calculationReport;
        }
    }
}
