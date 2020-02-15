namespace ModelAnalyzer.Parameters.Events.Weight
{
    class BackRelationsWeightCoef : FloatSingleParameter
    {
        public BackRelationsWeightCoef()
        {
            type = ParameterType.In;
            title = "Коэф. веса связей назад";
            details = "Коэффициент используемый при расчете вклада связей в вес события, если событие имеет связи только назад";
            fractionalDigits = 0;
            tags.Add(ParameterTag.events);
            tags.Add(ParameterTag.eventsWeight);
        }
    }
}
