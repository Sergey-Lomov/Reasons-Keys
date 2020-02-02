namespace ModelAnalyzer.Parameters.Items.Artifacts.SymmetricalStabiliser
{
    class SS_UsageAmount : FloatSingleParameter
    {
        public SS_UsageAmount()
        {
            type = ParameterType.In;
            title = "СС: кол-во использований";
            details = "";
            fractionalDigits = 0;
            tags.Add(ParameterTag.items);
            tags.Add(ParameterTag.artifacts);
        }
    }
}
