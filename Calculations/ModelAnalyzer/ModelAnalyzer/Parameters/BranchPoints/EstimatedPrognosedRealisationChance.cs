namespace ModelAnalyzer.Parameters.BranchPoints
{
    class EstimatedPrognosedRealisationChance : FloatSingleParameter
    {
        public EstimatedPrognosedRealisationChance()
        {
            type = ParameterType.In;
            title = "Оценка шанса реализации по задумке организатора";
            details = "Задает ожидаемый шанс того, что событие будет реализовано именно тем способом, который задумывал его организатор";
            fractionalDigits = 2;
            tags.Add(ParameterTag.branchPoints);
        }
    }
}
