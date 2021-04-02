namespace ModelAnalyzer.Parameters.Items.Artifacts.LachesisNeedle
{
    class LN_MaxRadius : FloatSingleParameter
    {
        public LN_MaxRadius()
        {
            type = ParameterType.In;
            title = "ИЛ: максимальный радиус";
            details = "Максимальный радиус применения, который может получить ИЛ в результате рассчетов";
            fractionalDigits = 0;
            tags.Add(ParameterTag.items);
            tags.Add(ParameterTag.artifacts);
        }
    }
}
