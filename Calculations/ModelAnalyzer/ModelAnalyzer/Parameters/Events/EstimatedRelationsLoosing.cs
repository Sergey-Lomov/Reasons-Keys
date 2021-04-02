namespace ModelAnalyzer.Parameters.Events
{
    class EstimatedRelationsLoosing : FloatSingleParameter
    {
        public EstimatedRelationsLoosing()
        {
            type = ParameterType.In;
            title = "Оценка вероятности осознанной потери связи";
            details = "Задает то, с какой вероятностью (ожидаемой) игроки будут располагать события таким образом, что какая-либо из связей будет вести в пустой узел, который не будет заполнен событием до момента реализации.";
            fractionalDigits = 2;
            tags.Add(ParameterTag.activities);
            tags.Add(ParameterTag.events);
        }
    }
}
