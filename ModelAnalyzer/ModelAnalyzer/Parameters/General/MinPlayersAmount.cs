namespace ModelAnalyzer.Parameters.General
{
    class MinPlayersAmount : FloatSingleParameter
    {
        public MinPlayersAmount()
        {
            type = ParameterType.In;
            title = "Минимальное кол-во игроков";
            details = "";
            fractionalDigits = 0;
            tags.Add(ParameterTag.general);
        }
    }
}
