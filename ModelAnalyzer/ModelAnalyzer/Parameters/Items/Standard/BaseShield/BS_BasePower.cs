namespace ModelAnalyzer.Parameters.Items.Standard.BaseShield
{
    class BS_BasePower : SingleParameter
    {
        public BS_BasePower()
        {
            type = ParameterType.In;
            title = "БЩ: базовая мощность";
            details = "Мощность БЩ без улучшений";
            fractionalDigits = 2;
            tags.Add(ParameterTag.items);
            tags.Add(ParameterTag.baseItems);
        }
    }
}
