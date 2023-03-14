
namespace ModelAnalyzer.Parameters.Events
{
    class BranchPointsEndlessRandomization : BoolParameter
    {
        public BranchPointsEndlessRandomization()
        {
            type = ParameterType.In;
            title = "Неограниченная рандомизация при балансировке очков ветвей";
            details = "Если флаг установлен лимит будет проигнорирован и рандомизация будет продолжаться до тех пора, пока не будет найдена колода с приемлемым балансом";
            tags.Add(ParameterTag.events);
            tags.Add(ParameterTag.branchPoints);
        }
    }
}
