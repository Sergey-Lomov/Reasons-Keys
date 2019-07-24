using ModelAnalyzer.Parameters.Activities;

namespace ModelAnalyzer.Parameters.Events.Weight
{
    class BranchPointWeight : SingleParameter
    {
        public BranchPointWeight()
        {
            type = ParameterType.Inner;
            title = "Вес очка ветви";
            details = "Имеется ввиду ТЗ эквивалент ондого очка ветви игрока";
            fractionalDigits = 2;
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float tp = calculator.UpdatedSingleValue(typeof(TotalPotential));
            float aecbp = calculator.UpdatedSingleValue(typeof(AverageEventsConcreteBranchPoints));

            value = unroundValue = tp / aecbp;

            return calculationReport;
        }
    }
}
