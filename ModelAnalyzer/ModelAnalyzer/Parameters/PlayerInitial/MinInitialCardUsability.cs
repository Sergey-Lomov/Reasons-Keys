namespace ModelAnalyzer.Parameters.PlayerInitial
{
    class MinInitialCardUsability : SingleParameter
    {
        public MinInitialCardUsability()
        {
            type = ParameterType.In;
            title = "Минимальная применимость изначальных событий";
            details = "Задает нижнюю границу применимости, которую могут получить изначальные события при генерации. Это ограничение не распространяется на логистическое изначальное событие.";
            fractionalDigits = 2;
            tags.Add(ParameterTag.playerInitial);
        }
    }
}
