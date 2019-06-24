namespace ModelAnalyzer.Parameters.Events
{
    class BlockRelationsCoef_OB : SingleParameter
    {
        public BlockRelationsCoef_OB()
        {
            type = ParameterType.In;
            title = "Коэф. связей-блокираторов (только назад)";
            details = "Задает отношение кол-ва связей-блокираторов среди всех связей. Но только для карт не имеющих связей вперед.";
            fractionalDigits = 2;
        }
    }
}
