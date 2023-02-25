using System;
using System.Collections.Generic;
using ModelAnalyzer.Services;

namespace ModelAnalyzer.Parameters.BranchPoints
{
    class InitialBP : FloatArrayParameter
    {
        public InitialBP()
        {
            type = ParameterType.Out;
            title = "Изначальное кол-во очков ветви";
            details = "";
            fractionalDigits = 0;
            tags.Add(ParameterTag.branchPoints);
            tags.Add(ParameterTag.playerInitial);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            List<float> egbpList = RequestParameter<EstimatedGameBP>(calculator).GetValue();
            float mbplc = RequestParameter<MaxBPLoosingCoefficient>(calculator).GetValue();

            if (!calculationReport.IsSuccess)
                return calculationReport;

            ClearValues();

            foreach (float egbp in egbpList)
            {
                var unroundValue = egbp * mbplc;
                unroundValues.Add(unroundValue);
                values.Add((float)Math.Round(unroundValue, MidpointRounding.AwayFromZero));
            }

            return calculationReport;
        }
    }
}
