namespace ModelAnalyzer.Parameters.Timing
{
    class AUMoveAmount : SingleParameter
    {
        public AUMoveAmount()
        {
            type = ParameterType.In;
            title = "Кол-во ЕА на ход";
            details = "";
            fractionalDigits = 0;
            tags.Add(ParameterTag.timing);
        }
    }
}
