using ModelAnalyzer.Services;
using ModelAnalyzer.Parameters.PlayerInitial;

namespace ModelAnalyzer.Parameters.Events.Weight
{
    class PassivePlayerWeight : FloatSingleParameter
    {
        public PassivePlayerWeight()
        {
            type = ParameterType.Inner;
            title = "Вес пассивного игрока";
            details = "Пассивным игроком называется пассивный союзник/проитвник - игрок, которого очки ветвей склоняют поддерживать или противодействовать томучтобы некоторое событие случилось.";
            fractionalDigits = 2;
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float bpw = calculator.UpdatedParameter<BranchPointWeight>().GetValue();
            float kebpc = calculator.UpdatedParameter<KeyEventsBranchPointsCoefficient>().GetValue();

            value = unroundValue = 1 / kebpc * bpw;

            return calculationReport;
        }
    }
}
