namespace ModelAnalyzer.Parameters.Items.Standard.BaseWeapon
{
    class BW_UpgradesAmount : SingleParameter
    {
        public BW_UpgradesAmount()
        {
            type = ParameterType.In;
            title = "БО: кол-во улучшений";
            details = "Задает кол-во улучшений базового оружия";
            fractionalDigits = 0;
            tags.Add(ParameterTag.items);
            tags.Add(ParameterTag.baseItems);
        }
    }
}
