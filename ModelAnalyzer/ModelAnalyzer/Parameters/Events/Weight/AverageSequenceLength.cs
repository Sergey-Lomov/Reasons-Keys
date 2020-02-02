namespace ModelAnalyzer.Parameters.Events.Weight
{
    class AverageSequenceLength : FloatSingleParameter
    {
        public AverageSequenceLength()
        {
            type = ParameterType.In;
            title = "Средняя длина цепочки";
            details = "Средняя длина цепочки событий";
            fractionalDigits = 2;
            tags.Add(ParameterTag.events);
            tags.Add(ParameterTag.eventsWeight);
        }
    }
}
