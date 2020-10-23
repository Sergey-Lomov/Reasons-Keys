namespace ModelAnalyzer.Parameters.Timing
{
    class FirstRoundAttackers : FloatSingleParameter
    {
        public FirstRoundAttackers()
        {
            type = ParameterType.In;
            title = "Кол-во атакующих в первый ход";
            details = "Подразумевается первый ход, после завершения периода безопасности";
            fractionalDigits = 0;
            tags.Add(ParameterTag.timing);
        }
    }
}
