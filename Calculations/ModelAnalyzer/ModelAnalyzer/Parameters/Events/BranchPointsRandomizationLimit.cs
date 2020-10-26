namespace ModelAnalyzer.Parameters.Events
{
    class BranchPointsRandomizationLimit : FloatSingleParameter
    {
        public BranchPointsRandomizationLimit()
        {
            type = ParameterType.In;
            title = "Кол-во итераций при балансировке очков ветвей";
            details = "Задает кол-во итераций при генерации случайных временных колод на основе колоды из параметра “ядро колоды событий континуума”";
            fractionalDigits = 0;
            tags.Add(ParameterTag.events);
            tags.Add(ParameterTag.branchPoints);
        }
    }
}
