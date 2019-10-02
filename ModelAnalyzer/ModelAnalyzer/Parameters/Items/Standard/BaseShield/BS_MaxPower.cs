namespace ModelAnalyzer.Parameters.Items.Standard.BaseShield
{
    class BS_MaxPower : FloatSingleParameter
    {
        public BS_MaxPower()
        {
            type = ParameterType.In;
            title = "БЩ: максимальная мощность";
            details = "Мощность БЩ со всеми улучшениями";
            fractionalDigits = 2;
            tags.Add(ParameterTag.items);
            tags.Add(ParameterTag.baseItems);
        }
    }
}
