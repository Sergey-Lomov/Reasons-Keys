using System;

namespace ModelAnalyzer.Parameters.Events.Weight
{
    class AverageRelationStability : SingleParameter
    {
        public AverageRelationStability()
        {
            type = ParameterType.Inner;
            title = "Средняя стабильность, переносимая связью";
            details = "";
            fractionalDigits = 2;
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float asl = calculator.UpdatedSingleValue(typeof(AverageSequenceLength));
            float asi = calculator.UpdatedSingleValue(typeof(AverageStabilityIncrement));

            float floorSum = 0;
            float floor_asl = (float)Math.Floor(asl);
            for (int i = 1; i <= Math.Floor(asl); i++)
                floorSum += i;

            float ceilSum = 0;
            float ceil_asl = (float)Math.Ceiling(asl);
            for (int i = 1; i <= Math.Ceiling(asl); i++)
                ceilSum += i;

            float floorStubility = floorSum * asi / floor_asl;
            float ceilStubility = ceilSum * asi / ceil_asl;

            value = unroundValue = floorStubility * (asl - floor_asl) + ceilStubility * (ceil_asl - asl);

            return calculationReport;
        }
    }
}
