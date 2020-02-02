using System.Collections.Generic;

using ModelAnalyzer.Services;
using ModelAnalyzer.Parameters.Activities;

namespace ModelAnalyzer.Parameters.PlayerInitial
{
    class RealKeyEventBrachPointCoefficients : FloatArrayParameter
    {
        public RealKeyEventBrachPointCoefficients()
        {
            type = ParameterType.Indicator;
            title = "Реальные коэф. очков ветвей на решающих событиях";
            details = "Кол-во очков ветвей на решающих событиях считается усредненным для всех возможных количеств игроков. Поэтому реальное значение коэф. очков ветвей на решающих событиях отличаетяс от одноименного входящего параметра.";
            fractionalDigits = 2;
            tags.Add(ParameterTag.playerInitial);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float ketbp = RequestParmeter<KeyEventsTotalBrachPoints>(calculator).GetValue();
            List<float> auecbp = RequestParmeter<AverageUnkeyEventsConcreteBranchPoints>(calculator).GetValue();

            if (!calculationReport.IsSuccess)
                return calculationReport;

            List<float> result = new List<float>(auecbp.Count);
            foreach (float current_auecbp in auecbp)
                result.Add(ketbp / current_auecbp);

            values = unroundValues = result;

            return calculationReport;
        }
    }
}
