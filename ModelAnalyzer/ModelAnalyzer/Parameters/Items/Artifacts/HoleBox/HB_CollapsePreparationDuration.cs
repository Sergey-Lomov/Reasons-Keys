namespace ModelAnalyzer.Parameters.Items.Artifacts.HoleBox
{
    class HB_CollapsePreparationDuration : SingleParameter
    {
        public HB_CollapsePreparationDuration()
        {
            type = ParameterType.Indicator;
            title = "ДК: длительность подготовки коллапса";
            details = "Кол-во раундов, которое нужно для доведения артефакта до коллапса.";
            fractionalDigits = 0;
            tags.Add(ParameterTag.items);
            tags.Add(ParameterTag.artifacts);
        }

        internal override ParameterCalculationReport Calculate(Calculator calculator)
        {
            calculationReport = new ParameterCalculationReport(this);

            float tisa = calculator.UpdatedSingleValue(typeof(HB_TensionInreasingStepsAmount));

            value = unroundValue = tisa + 2;

            return calculationReport;
        }
    }
}
