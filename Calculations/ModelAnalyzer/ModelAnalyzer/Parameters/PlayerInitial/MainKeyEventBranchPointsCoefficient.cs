namespace ModelAnalyzer.Parameters.PlayerInitial
{
    class MainKeyEventBranchPointsCoefficient : FloatSingleParameter
    {
        public MainKeyEventBranchPointsCoefficient()
        {
            type = ParameterType.In;
            title = "Коэф. очков ветви главного решающего события";
            details = "Определяет во сколько раз главное решающее событие должно иметь больше очков, чем остальные решающие события ";
            fractionalDigits = 2;
            tags.Add(ParameterTag.playerInitial);
        }
    }
}
