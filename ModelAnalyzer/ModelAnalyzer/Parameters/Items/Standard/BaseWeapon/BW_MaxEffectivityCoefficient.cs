namespace ModelAnalyzer.Parameters.Items.Standard.BaseWeapon
{
    class BW_MaxEffectivityCoefficient : FloatSingleParameter
    {
        public BW_MaxEffectivityCoefficient()
        {
            type = ParameterType.In;
            title = "БО: коэф. максимальной эффективности";
            details = "Определяет во сколько раз эффективность базового роужия при наличии всех улучшений будут больше, чем его эффективность без улучшений";
            fractionalDigits = 2;
            tags.Add(ParameterTag.items);
            tags.Add(ParameterTag.baseItems);
        }
    }
}
