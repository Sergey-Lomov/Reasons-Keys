namespace ModelAnalyzer.Parameters.Events
{
    class BackBlockerCoefficient : FloatSingleParameter
    {
        public BackBlockerCoefficient()
        {
            type = ParameterType.In;
            title = "Коэф. блокираторов-назад";
            details = "";
            fractionalDigits = 2;
            tags.Add(ParameterTag.events);
        }
    }
}
