using System;

namespace ModelAnalyzer.Parameters.Moving
{
    class InitialSpeed : SingleParameter
    {
        public InitialSpeed()
        {
            type = ParameterType.Out;
            title = "Начальная скорость сфер";
            details = "";
            fractionalDigits = 0;
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float isc = calculator.GetUpdateSingleValue(typeof(InitialSpeedCoef));
            float ad = calculator.GetUpdateSingleValue(typeof(AverageDistance));

            unroundValue = ad * isc;
            value = (float) Math.Round(unroundValue);

            return calculationReport;
        }
    }
}
