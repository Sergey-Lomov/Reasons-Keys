using ModelAnalyzer.Services;

namespace ModelAnalyzer.Parameters.PlayerInitial
{
    class InitialStackSize : FloatSingleParameter
    {
        public InitialStackSize()
        {
            type = ParameterType.Out;
            title = "Размер изначальной раздачи";
            details = "";
            fractionalDigits = 0;
            tags.Add(ParameterTag.playerInitial);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            var cm = RequestModule<InitialStackCalculationModule>(calculator);
            value = unroundValue = cm.initialStackSize;

            return calculationReport;
        }
    }
}
