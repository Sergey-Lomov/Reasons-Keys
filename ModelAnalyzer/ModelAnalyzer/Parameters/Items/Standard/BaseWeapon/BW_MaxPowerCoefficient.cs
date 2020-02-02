namespace ModelAnalyzer.Parameters.Items.Standard.BaseWeapon
{
    class BW_MaxPowerCoefficient : FloatSingleParameter
    {
        public BW_MaxPowerCoefficient()
        {
            type = ParameterType.In;
            title = "БО: коэф. максимальной мощности";
            details = "Определяет во сколько раз мощность базового роужия при наличии всех улучшений будут больше, чем его мощность без улучшений";
            fractionalDigits = 2;
            tags.Add(ParameterTag.items);
            tags.Add(ParameterTag.baseItems);
        }
    }
}
