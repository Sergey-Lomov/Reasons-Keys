namespace ModelAnalyzer.Parameters.Timing
{
    class RoundAmount : FloatSingleParameter
    {
        public RoundAmount()
        {
            type = ParameterType.In;
            title = "Кол-во раундов в партии";
            details = "";
            fractionalDigits = 0;
            tags.Add(ParameterTag.timing);
        }
    }
}
