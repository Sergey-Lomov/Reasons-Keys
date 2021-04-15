namespace ModelAnalyzer.Parameters.Items.Standard.RelationsImprover
{
    class RI_ImprovementLimitCoefficient : FloatSingleParameter
    {
        public RI_ImprovementLimitCoefficient()
        {
            type = ParameterType.In;
            title = "УС: коэф. предела усиления";
            details = "Задает отношение значения “УС: предел усиления” к значению “Сила воздействия связей”";
            fractionalDigits = 2;
            tags.Add(ParameterTag.items);
            tags.Add(ParameterTag.baseItems);
        }
    }
}
