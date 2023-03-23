
namespace ModelAnalyzer.Parameters.Events
{
    class BranchPointsRandomizationSeed : FloatSingleParameter
    {
        public BranchPointsRandomizationSeed()
        {
            type = ParameterType.In;
            title = "Семя рандомизации для балансировки очков ветвей";
            details = "Позволяет указать семя для рандомизации при балансировке очков ветвей. Полезно для поиска приемлемой колоды в несклоько заходов.";
            fractionalDigits = 0;
            tags.Add(ParameterTag.events);
            tags.Add(ParameterTag.branchPoints);
        }
    }
}
