namespace ModelAnalyzer.Parameters.Events
{
    class StabilityIncrementAllocation : FloatArrayParameter
    {
        public StabilityIncrementAllocation()
        {
            type = ParameterType.In;
            title = "Распределение увеличения стабильности";
            details = "";
            fractionalDigits = 0;
            tags.Add(ParameterTag.events);
        }
    }
}
