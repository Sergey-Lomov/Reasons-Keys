using ModelAnalyzer.Services;
using ModelAnalyzer.Parameters.General;
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

            float maxpa = RequestParmeter<MaxPlayersAmount>(calculator).GetValue();
            float minpa = RequestParmeter<MinPlayersAmount>(calculator).GetValue();
            float mauc = RequestParmeter<MiningAUCoef>(calculator).GetValue();
            float aupa = RequestParmeter<AUPartyAmount>(calculator).GetValue();
            float cna = RequestParmeter<ContinuumNodesAmount>(calculator).GetValue();

            float averagePlayersAmount = (maxpa + minpa) / 2;
            value = unroundValue = aupa * mauc * averagePlayersAmount / cna; ;

            return calculationReport;
        }
    }
}
