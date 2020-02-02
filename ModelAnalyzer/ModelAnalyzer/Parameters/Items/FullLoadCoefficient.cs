namespace ModelAnalyzer.Parameters.Items
{
    class FullLoadCoefficient : FloatSingleParameter
    {
        public FullLoadCoefficient()
        {
            type = ParameterType.In;
            title = "Коэф. времени полной закупки";
            details = "Указывает отношение раундов необходимых на покупку всех предметов к общему числу раундов";
            fractionalDigits = 2;
            tags.Add(ParameterTag.items);
        }
    }
}
