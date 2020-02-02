using ModelAnalyzer.Services;

namespace ModelAnalyzer.Parameters.Items.Artifacts.CollectorModule
{
    class CM_Power : FloatSingleParameter
    {
        public CM_Power()
        {
            type = ParameterType.Out;
            title = "МК: мощность";
            details = "Кол-во соседних узлов, на которое может быть применен модуль-коллектор";
            fractionalDigits = 2;
            tags.Add(ParameterTag.items);
            tags.Add(ParameterTag.artifacts);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            var module = calculator.UpdatedModule<CM_CalculationModule>();
            value = unroundValue = module.power;

            return calculationReport;
        }
    }
}
