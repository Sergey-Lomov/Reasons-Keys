namespace ModelAnalyzer.Parameters.Events
{
    class EventMaxRelations : SingleParameter
    {
        public EventMaxRelations()
        {
            type = ParameterType.In;
            title = "Лимит связей события";
            details = "Ни одно событие континуума не может иметь связей больше, чем знаение этого параметра";
            fractionalDigits = 0;
        }
    }
}
