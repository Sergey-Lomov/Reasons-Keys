namespace ModelAnalyzer.Parameters.PlayerInitial
{
    class KeyEventsAmount : SingleParameter
    {
        public KeyEventsAmount()
        {
            type = ParameterType.In;
            title = "Кол-во ключевых событий";
            details = "Имеется ввиду кол-во ключевых событий, которое имеется в начале игры у каждого игрока.";
            fractionalDigits = 0;
            tags.Add(ParameterTag.playerInitial);
        }
    }
}
