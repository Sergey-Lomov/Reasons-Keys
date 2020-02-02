using System;

using ModelAnalyzer.Services;
using ModelAnalyzer.Parameters.Topology;

namespace ModelAnalyzer.Parameters.Moving
{
    class InitialSpeed : FloatSingleParameter
    {
        public InitialSpeed()
        {
            type = ParameterType.Out;
            title = "Начальная скорость сфер";
            details = "";
            fractionalDigits = 0;
            tags.Add(ParameterTag.moving);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float isc = RequestParmeter<InitialSpeedCoef>(calculator).GetValue();
            float ad = RequestParmeter<AverageDistance>(calculator).GetValue();

            unroundValue = ad * isc;
            value = (float) Math.Round(unroundValue);

            return calculationReport;
        }
    }
}
