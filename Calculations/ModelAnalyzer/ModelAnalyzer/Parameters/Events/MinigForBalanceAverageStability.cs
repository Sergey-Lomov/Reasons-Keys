namespace ModelAnalyzer.Parameters.Events
{
    class MinigForBalanceAverageStability : FloatSingleParameter
    {
        public MinigForBalanceAverageStability()
        {
            type = ParameterType.In;
            title = "Кол-во актов добычи для уравновешивания средней стабильности";
            details = "Этот параметр задает кол-во актов добычи (средней добычи), необходимых для получения ТЗ, уравновешивающего среднестатистическую стабильности события.";
            fractionalDigits = 0;
            tags.Add(ParameterTag.events);
        }
    }
}
