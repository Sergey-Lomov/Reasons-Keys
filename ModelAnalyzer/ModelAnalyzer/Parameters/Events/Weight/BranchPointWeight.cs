using ModelAnalyzer.Services;
using ModelAnalyzer.Parameters.Activities;

namespace ModelAnalyzer.Parameters.Events.Weight
{
    class BranchPointWeight : FloatSingleParameter
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

            float tp = RequestParmeter<TotalPotential>(calculator).GetValue();
            float aecbp = RequestParmeter<AverageEventsConcreteBranchPoints>(calculator).GetValue();

            value = unroundValue = tp / aecbp;

            return calculationReport;
        }
    }
}
