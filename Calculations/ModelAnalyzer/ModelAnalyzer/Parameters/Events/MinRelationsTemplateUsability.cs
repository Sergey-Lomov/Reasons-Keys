namespace ModelAnalyzer.Parameters.Events
{
    class MinRelationsTemplateUsability : FloatSingleParameter
    {
        public MinRelationsTemplateUsability()
        {
            type = ParameterType.In;
            title = "Минимальная применимость шаблона связей";
            details = "Шаблоны расположения связей имеющие применимость ниже заданной не будут представлены в колоде континуума";
            fractionalDigits = 2;
            tags.Add(ParameterTag.events);
        }
    }
}
