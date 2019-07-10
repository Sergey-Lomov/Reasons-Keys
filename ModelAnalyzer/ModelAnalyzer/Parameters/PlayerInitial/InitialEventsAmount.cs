namespace ModelAnalyzer.Parameters.PlayerInitial
{
    class InitialEventsAmount : SingleParameter
    {
        public InitialEventsAmount()
        {
            type = ParameterType.In;
            title = "Кол-во изначальных событий";
            details = "Имеется ввиду, кол-во не решающих событий в начальной радзаче каждого игрока";
            fractionalDigits = 0;
        }
    }
}
