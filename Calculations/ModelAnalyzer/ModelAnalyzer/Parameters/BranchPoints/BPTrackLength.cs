using ModelAnalyzer.Services;
using System;
using System.Linq;
using System.Collections.Generic;

namespace ModelAnalyzer.Parameters.BranchPoints
{
    class BPTrackLength : FloatSingleParameter
    {
        public BPTrackLength()
        {
            type = ParameterType.Out;
            title = "Длина трека очков ветвей";
            details = "";
            fractionalDigits = 0;
            tags.Add(ParameterTag.branchPoints);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float mgbp = RequestParmeter<MaxGameBP>(calculator).GetValue();
            List<float> ibp = RequestParmeter<InitialBP>(calculator).GetValue();

            if (!calculationReport.IsSuccess)
                return calculationReport;

            unroundValue = mgbp + ibp.Max();
            value = (float)Math.Round(unroundValue, MidpointRounding.AwayFromZero);

            return calculationReport;
        }
    }
}
