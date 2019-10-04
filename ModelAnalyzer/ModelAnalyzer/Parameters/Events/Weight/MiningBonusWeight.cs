using ModelAnalyzer.Services;
using ModelAnalyzer.Parameters.Topology;
using ModelAnalyzer.Parameters.Timing;
using ModelAnalyzer.Parameters.Mining;

namespace ModelAnalyzer.Parameters.Events.Weight
{
    class MiningBonusWeight : FloatSingleParameter
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

            float maxpa = calculator.UpdatedParameter<MaxPlayersAmount>().GetValue();
            float minpa = calculator.UpdatedParameter<MinPlayersAmount>().GetValue();
            float mauc = calculator.UpdatedParameter<MiningAUCoef>().GetValue();
            float aupa = calculator.UpdatedParameter<AUPartyAmount>().GetValue();
            float cna = calculator.UpdatedParameter<ContinuumNodesAmount>().GetValue();

            float averagePlayersAmount = (maxpa + minpa) / 2;
            value = unroundValue = aupa * mauc * averagePlayersAmount / cna; ;

            return calculationReport;
        }
    }
}
