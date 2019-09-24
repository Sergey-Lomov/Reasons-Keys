namespace ModelAnalyzer.Parameters.Activities
{
    class KeyEventCreationAmount : SingleParameter
    {
        public KeyEventCreationAmount()
        {
            type = ParameterType. In;
            title = "Стандартное кол-во организации решающих событий";
            details = "Это те события, которые игроки стараются организовать с помощью событий континуума и изначальных событий. Такие события игроки имеют в руке на старте, они имеют большое количество очков ветви игрока и специальную пометку.";
            fractionalDigits = 2;
            tags.Add(ParameterTag.activities);
        }
    }
}
