using ModelAnalyzer.Services;

namespace ModelAnalyzer.Parameters.Activities.EventsRestoring
{
    class MinimalLuckyStackChance : FloatSingleParameter
    {
        public MinimalLuckyStackChance()
        {
            type = ParameterType.In;
            title = "Минимальный шанс благоприятной раздачи";
            details = "Кол-ва карт из которых игрок выбирает новое событие (после организации события из руки) будет подобрано таким образом, чтобы шанс обнаружить среди них событие с -1/-1 для противников или +1 для себя был не ниже значения этого параметра";
            fractionalDigits = 2;
            tags.Add(ParameterTag.activities);
        }
    }
}
