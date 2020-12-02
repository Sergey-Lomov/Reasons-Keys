namespace ModelAnalyzer.Parameters.Events
{
    class FrontBlockerCoefficient : FloatSingleParameter
    {
        public FrontBlockerCoefficient()
        {
            type = ParameterType.In;
            title = "Коэф. блокираторов-вперед";
            details = "";
            fractionalDigits = 2;
            tags.Add(ParameterTag.events);
        }
    }
}
