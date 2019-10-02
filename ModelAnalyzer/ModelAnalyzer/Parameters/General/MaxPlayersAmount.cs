namespace ModelAnalyzer.Parameters
{
    class MaxPlayersAmount : FloatSingleParameter
    {
        public MaxPlayersAmount()
        {
            type = ParameterType.In;
            title = "Максимальное кол-во игроков";
            details = "";
            fractionalDigits = 0;
            tags.Add(ParameterTag.general);
        }
    }
}
