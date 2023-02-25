using ModelAnalyzer.Services;
using ModelAnalyzer.Parameters.General;
using ModelAnalyzer.Parameters.Topology;
using ModelAnalyzer.Parameters.Activities;

namespace ModelAnalyzer.Parameters.Events.Weight
{
    class MiningBonusWeight : FloatSingleParameter
    {
        public MiningBonusWeight()
        {
            type = ParameterType.Inner;
            title = "Вес бонуса добычи";
            details = "Имеется ввиду вес ондой единицы бонуса добычи";
            fractionalDigits = 2;
            tags.Add(ParameterTag.events);
            tags.Add(ParameterTag.eventsWeight);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float maxpa = RequestParameter<MaxPlayersAmount>(calculator).GetValue();
            float minpa = RequestParameter<MinPlayersAmount>(calculator).GetValue();
            float ma = RequestParameter<MiningAmount>(calculator).GetValue();
            float cna = RequestParameter<ContinuumNodesAmount>(calculator).GetValue();

            float averagePlayersAmount = (maxpa + minpa) / 2;
            value = unroundValue = ma * averagePlayersAmount / cna; ;

            return calculationReport;
        }
    }
}
