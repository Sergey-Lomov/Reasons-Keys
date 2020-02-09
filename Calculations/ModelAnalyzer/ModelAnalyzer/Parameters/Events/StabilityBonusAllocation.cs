namespace ModelAnalyzer.Parameters.Events
{
    class StabilityBonusAllocation : FloatArrayParameter
    {
        public StabilityBonusAllocation()
        {
            type = ParameterType.In;
            title = "Распределение бонусов стабильности";
            details = "";
            fractionalDigits = 0;
            tags.Add(ParameterTag.events);
        }
    }
}
