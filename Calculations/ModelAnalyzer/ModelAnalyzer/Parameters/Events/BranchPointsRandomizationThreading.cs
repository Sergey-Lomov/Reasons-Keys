namespace ModelAnalyzer.Parameters.Events
{
    class BranchPointsRandomizationThreading : BoolParameter
    {
        public BranchPointsRandomizationThreading()
        {
            type = ParameterType.In;
            title = "Многопоточная балансировка ветвей [временно]";
            editorLabel = "Многопоточность";
            details = "Определяет должна ли использоваться при балансировке ветвей многопоточная реализация или нет. Временный параметр, должен быть удален после того, как будет завершено тестирование многопоточной реализации.";
            tags.Add(ParameterTag.events);
            tags.Add(ParameterTag.branchPoints);
        }
    }
}
