﻿using System;
using System.Collections.Generic;
using ModelAnalyzer.Services;
using System.Linq;

namespace ModelAnalyzer.Parameters.BranchPointsTrack
{
    class InitialBP : FloatArrayParameter
    {
        public InitialBP()
        {
            type = ParameterType.Out;
            title = "Изначальное количество очков ветви";
            details = "";
            fractionalDigits = 0;
            tags.Add(ParameterTag.branchPoints);
            tags.Add(ParameterTag.playerInitial);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            List<float> egbpList = RequestParmeter<EstimatedGameBP>(calculator).GetValue();
            float mbplc = RequestParmeter<MaxBPLoosingCoefficient>(calculator).GetValue();

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
