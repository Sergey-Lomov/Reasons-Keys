namespace ModelAnalyzer.Parameters.Activities
{
    class EventCreationAmount : SingleParameter
    {
        public EventCreationAmount()
        {
            type = ParameterType.In;
            title = "Стандартное кол-во организации событий";
            details = "Предполагается, что в течении партии игрок будет организовывать в среднем указанное кол-во событий.";
            fractionalDigits = 1;
        }
    }
}
