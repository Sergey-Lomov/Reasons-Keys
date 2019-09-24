namespace ModelAnalyzer.Parameters.Timing
{
    class MoveDuration : SingleParameter
    {
        public MoveDuration()
        {
            type = ParameterType.In;
            title = "Рекомендуемое ограничение хода (мин)";
            details = "Игроки вольны выбрать любое ограничение или отказаться от него вовсе. Но именно при таком ограничении общее время партии будет примерно равно рассчитанному.";
            fractionalDigits = 2;
            tags.Add(ParameterTag.timing);
        }
    }
}
