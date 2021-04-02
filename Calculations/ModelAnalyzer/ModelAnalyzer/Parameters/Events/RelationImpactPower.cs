namespace ModelAnalyzer.Parameters.Events
{
    class RelationImpactPower : FloatSingleParameter
    {
        public RelationImpactPower()
        {
            type = ParameterType.In;
            title = "Сила воздействия связей";
            details = "Этот параметр задает то, насколько сильно воздействие связи относительно воздействия игрока";
            fractionalDigits = 0;
            tags.Add(ParameterTag.events);
        }
    }
}
