namespace ModelAnalyzer.Parameters.PlayerInitial
{
    class AverageInitialEventsBranchPoints : FloatSingleParameter
    {
        public AverageInitialEventsBranchPoints()
        {
            type = ParameterType.In;
            title = "Среднее кол-во очков ветвей на изначальных событиях";
            details = "На данный момент изначальные карты не присутствуют в системе и этот параметр не может быть расчитан. В будущем скорее всего он будет внутренним а не входящим.";
            fractionalDigits = 2;
            tags.Add(ParameterTag.playerInitial);
        }
    }
}
