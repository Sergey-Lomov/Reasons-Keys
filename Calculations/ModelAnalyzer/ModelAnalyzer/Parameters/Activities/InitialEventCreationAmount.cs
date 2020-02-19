namespace ModelAnalyzer.Parameters.Activities
{
    class InitialEventCreationAmount : FloatSingleParameter
    {
        public InitialEventCreationAmount()
        {
            type = ParameterType.In;
            title = "Стандартное кол-во организации изначальных событий";
            details = "";
            fractionalDigits = 2;
            tags.Add(ParameterTag.activities);
        }
    }
}
