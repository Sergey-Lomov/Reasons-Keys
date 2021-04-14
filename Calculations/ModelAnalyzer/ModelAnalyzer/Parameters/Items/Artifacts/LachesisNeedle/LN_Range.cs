using ModelAnalyzer.Services;

namespace ModelAnalyzer.Parameters.Items.Artifacts.LachesisNeedle
{
    class LN_Range : FloatSingleParameter
    {
        public LN_Range()
        {
            type = ParameterType.Out;
            title = "ИЛ: радиус действия";
            details = "Артефакт позволяет соединять причинно-следственными связями пару узлов с помощью маркеров, если они расположены на расстоянии не большем, чем задает этот параметр";
            fractionalDigits = 2;
            tags.Add(ParameterTag.items);
            tags.Add(ParameterTag.artifacts);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            var cm = RequestModule<LN_CalculationModule>(calculator);
            value = unroundValue = cm.range;

            return calculationReport;
        }
    }
}
