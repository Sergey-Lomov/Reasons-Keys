using ModelAnalyzer.Parameters.Timing;
using ModelAnalyzer.Parameters.Mining;

namespace ModelAnalyzer.Parameters.Events.Weight
{
    class MiningBonusWeight : SingleParameter
    {
        public MiningBonusWeight()
        {
            type = ParameterType.Inner;
            title = "Вес бонуса добычи";
            details = "Имеется ввиду вес ондой единицв бонуса добычи";
            fractionalDigits = 2;
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float maxpa = calculator.UpdatedSingleValue(typeof(MaxPlayersAmount));
            float minpa = calculator.UpdatedSingleValue(typeof(MinPlayersAmount));
            float mauc = calculator.UpdatedSingleValue(typeof(MiningAUCoef));
            float aupa = calculator.UpdatedSingleValue(typeof(AUPartyAmount));
            float cna = calculator.UpdatedSingleValue(typeof(ContinuumNodesAmount));

            float averagePlayersAmount = (maxpa + minpa) / 2;
            value = unroundValue = aupa * mauc * averagePlayersAmount / cna; ;

            return calculationReport;
        }
    }
}
