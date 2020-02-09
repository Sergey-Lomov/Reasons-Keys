namespace ModelAnalyzer.Parameters.Events
{
    class ChainStabilityLimit : FloatSingleParameter
    {
        public ChainStabilityLimit()
        {
            type = ParameterType.In;
            title = "Предел цепной стабильности";
            details = "Это параметр задает предел, превысив который цепная стабильность опускается до 1.";
            fractionalDigits = 0;
            tags.Add(ParameterTag.events);
        }
    }
}
