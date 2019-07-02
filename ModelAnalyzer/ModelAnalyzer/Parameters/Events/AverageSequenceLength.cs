namespace ModelAnalyzer.Parameters.Events
{
    class AverageSequenceLength : SingleParameter
    {
        public AverageSequenceLength()
        {
            type = ParameterType.In;
            title = "Средняя длина цепочки";
            details = "Средняя длина цепочки событий";
            fractionalDigits = 2;
        }
    }
}
