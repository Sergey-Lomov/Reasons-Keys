namespace ModelAnalyzer.Parameters.Events
{
    class FrontRelationsCoef : FloatSingleParameter
    {
        public FrontRelationsCoef()
        {
            type = ParameterType.In;
            title = "Коэф. связей вперед";
            details = "Определяет как много карт континуума должны иметь связи вперед";
            fractionalDigits = 2;
            tags.Add(ParameterTag.events);
        }
    }
}
