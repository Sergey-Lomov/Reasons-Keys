using System;
using System.Linq;
using System.Collections.Generic;

using ModelAnalyzer.Services;
using ModelAnalyzer.Parameters.Activities;

namespace ModelAnalyzer.Parameters.PlayerInitial
{
    class KeyEventsTotalBrachPoints : FloatSingleParameter
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

            float kebpc = calculator.UpdatedParameter<KeyEventsBranchPointsCoefficient>().GetValue();
            List<float> auecbp = calculator.UpdatedParameter<AverageUnkeyEventsConcreteBranchPoints>().GetValue();

            unroundValue = auecbp.Sum() / auecbp.Count * kebpc;
            value = (float)Math.Round(unroundValue, MidpointRounding.AwayFromZero);

            return calculationReport;
        }
    }
}
