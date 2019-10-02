namespace ModelAnalyzer.Parameters.Activities
{
    class EventActionsCoef : FloatSingleParameter
    {
        public EventActionsCoef()
        {
            type = ParameterType.In;
            title = "Коэф. событийных действий";
            details = "Задает отношение между остатком полного потенциала (после вычета ЕА на добычу и ТЗ на перемещение) и полной стоимость всех событийных действий (организация и воздействие) на протяжении партии.";
            fractionalDigits = 2;
            tags.Add(ParameterTag.activities);
        }
    }
}
