namespace ModelAnalyzer.Parameters
{
    class MaxPlayersAmount : SingleParameter
    {
        public MaxPlayersAmount()
        {
            type = ParameterType.In;
            title = "Максимальное кол-во игроков";
            details = "";
            fractionalDigits = 0;
        }
    }
}
