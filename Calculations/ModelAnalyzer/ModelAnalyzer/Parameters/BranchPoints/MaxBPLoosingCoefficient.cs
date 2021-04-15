namespace ModelAnalyzer.Parameters.BranchPoints
{
    class MaxBPLoosingCoefficient : FloatSingleParameter
    {
        public MaxBPLoosingCoefficient()
        {
            type = ParameterType.In;
            title = "Коэф. максимальной потери очков ветви";
            details = "Задает отношение кол-ва очков, которые можно потерять не получив ни одного +1 к расчетному кол-ву очков ветви, набираемому игроков за партию.";
            fractionalDigits = 2;
            tags.Add(ParameterTag.branchPoints);
        }
    }
}
