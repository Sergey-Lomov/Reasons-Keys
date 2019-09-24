namespace ModelAnalyzer.Parameters.Activities
{
    class EventImpactAmount : SingleParameter
    {
        public EventImpactAmount()
        {
            type = ParameterType.In;
            title = "Стандартное кол-во воздействий";
            details = "Предполагается, что в течении партии игрок будет в среднем воздействовать на события указанное кол-во раз.";
            fractionalDigits = 1;
            tags.Add(ParameterTag.activities);
        }
    }
}
