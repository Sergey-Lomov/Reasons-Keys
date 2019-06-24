namespace ModelAnalyzer.Parameters.Events
{
    class BlockEventsCoef_2D : SingleParameter
    {
        public BlockEventsCoef_2D()
        {
            type = ParameterType.In;
            title = "Коэф. событий-блокираторов (2 стороны)";
            details = "Определяет отношение карт с болкираторами к общему кол-ву карт. Но только для карт со связями вперед.";
            fractionalDigits = 2;
        }
    }
}
