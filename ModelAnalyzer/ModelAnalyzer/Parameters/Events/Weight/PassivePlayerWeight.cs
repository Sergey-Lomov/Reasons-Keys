using ModelAnalyzer.Parameters.PlayerInitial;

namespace ModelAnalyzer.Parameters.Events.Weight
{
    class PassivePlayerWeight : SingleParameter
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

            float bpw = calculator.UpdatedSingleValue(typeof(BranchPointWeight));
            float kebpc = calculator.UpdatedSingleValue(typeof(KeyEventsBrachPointsCoefficient));

            value = unroundValue = 1 / kebpc * bpw;

            return calculationReport;
        }
    }
}
