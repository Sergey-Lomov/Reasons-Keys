namespace ModelAnalyzer.Parameters.Items.Artifacts.LachesisNeedle
{
    class LN_MinRadius : FloatSingleParameter
    {
        public LN_MinRadius()
        {
            type = ParameterType.In;
            title = "ИЛ: минимальный радиус";
            details = "Минимальный радиус применения, который может получить ИЛ в результате рассчетов";
            fractionalDigits = 0;
            tags.Add(ParameterTag.items);
            tags.Add(ParameterTag.artifacts);
        }
    }
}
