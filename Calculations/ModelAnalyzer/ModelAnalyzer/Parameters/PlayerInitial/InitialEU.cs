using System;
using System.Collections.Generic;
using System.Linq;

using ModelAnalyzer.Services;
using ModelAnalyzer.Parameters.Topology;
using ModelAnalyzer.Parameters.Moving;
using ModelAnalyzer.Parameters.Activities;

namespace ModelAnalyzer.Parameters.PlayerInitial
{
    class InitialEU : FloatSingleParameter
    {
        public InitialEU()
        {
            type = ParameterType.Out;
            title = "Начальный запас ТЗ";
            details = "";
            fractionalDigits = 0;
            tags.Add(ParameterTag.playerInitial);
            tags.Add(ParameterTag.mining);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float r = RequestParameter<FieldRadius>(calculator).GetValue();
            float mp = RequestParameter<MotionPrice>(calculator).GetValue();
            float ecp_eu = RequestParameter<EventCreationPriceEU>(calculator).GetValue();

            if (!calculationReport.IsSuccess)
                return calculationReport;

            float goToEdge = r * mp + 1;
            float prepareEventAndReturn = ecp_eu + 2 * mp + 1;

            unroundValue = new List<float>{goToEdge, prepareEventAndReturn }.Max();
            value = (float)Math.Ceiling(unroundValue);

            return calculationReport;
        }
    }
}
