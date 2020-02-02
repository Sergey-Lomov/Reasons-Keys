namespace ModelAnalyzer.Parameters.Events
{
    class MinBackRelations : FloatSingleParameter
    {
        public MinBackRelations()
        {
            type = ParameterType.In;
            title = "Минимальное кол-во связей назад";
            details = "Ни одно событие конитнуума не может иметь связей назад меньше, чем указанно в этом параметре";
            fractionalDigits = 0;
            tags.Add(ParameterTag.events);
        }
    }
}
