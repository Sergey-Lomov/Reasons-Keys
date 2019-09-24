namespace ModelAnalyzer.Parameters.Moving
{
    class MotionFreeLevel : SingleParameter
    {
        public MotionFreeLevel()
        {
            type = ParameterType.In;
            title = "Уровень свободы перемещения";
            details = "Задает отношение средней добычи к расстоянию, которое должно быть можно перодолеть за добытые ТЗ. Но расстояние обозначается не в узлах, а в средних расстояниях. Например если этот параметр равен 2, это значит, что игрок должен иметь возможность преодолеть расстояние в два раза больше среднего, за ТЗ добытые за один раз (при средней добыче)";
            fractionalDigits = 2;
            tags.Add(ParameterTag.moving);
        }
    }
}
