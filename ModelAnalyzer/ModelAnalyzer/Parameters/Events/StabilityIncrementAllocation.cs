namespace ModelAnalyzer.Parameters.Events
{
    class StabilityIncrementAllocation : ArrayParameter
    {
        public StabilityIncrementAllocation()
        {
            type = ParameterType.In;
            title = "Распределение увеличения стабильности";
            details = "";
            fractionalDigits = 0;
        }
    }
}
