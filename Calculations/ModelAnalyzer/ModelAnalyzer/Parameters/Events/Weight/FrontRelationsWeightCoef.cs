namespace ModelAnalyzer.Parameters.Events.Weight
{
    class FrontRelationsWeightCoef : FloatSingleParameter
    {
        public FrontRelationsWeightCoef()
        {
            type = ParameterType.In;
            title = "Коэф. веса связей вперед";
            details = "Коэффициент используемый при расчете вклада связей в вес события, если событие имеет хотябы одну связь вперед";
            fractionalDigits = 2;
            tags.Add(ParameterTag.events);
            tags.Add(ParameterTag.eventsWeight);
        }
    }
}
