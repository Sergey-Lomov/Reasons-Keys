﻿using System.Collections.Generic;

using ModelAnalyzer.Parameters.Activities;

namespace ModelAnalyzer.Parameters.PlayerInitial
{
    class RealKeyEventBrachPointCoefficients : ArrayParameter
    {
        public RealKeyEventBrachPointCoefficients()
        {
            type = ParameterType.Indicator;
            title = "Реальные коэф. очков ветвей на решающих событиях";
            details = "Кол-во очков ветвей на решающих событиях считается усредненным для всех возможных количеств игроков. Поэтому реальное значение коэф. очков ветвей на решающих событиях отличаетяс от одноименного входящего параметра.";
            fractionalDigits = 2;
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float ketbp = calculator.UpdateSingleValue(typeof(KeyEventsTotalBrachPoints));
            float[] auecbp = calculator.UpdateArrayValue(typeof(AverageUnkeyEventsConcreteBranchPoints));

            List<float> result = new List<float>(auecbp.Length);
            foreach (float current_auecbp in auecbp)
                result.Add(ketbp / current_auecbp);

            values = unroundValues = result;

            return calculationReport;
        }
    }
}
