namespace ModelAnalyzer.Parameters.Items
{
    class WeaponStandardPower : SingleParameter
    {
        public WeaponStandardPower()
        {
            type = ParameterType.In;
            title = "Оружие: стандартная мощность";
            details = "Точкой отсчета при расчетах связанных с оружие являются абстрактные “стандартные” значения мощности и эффективности. Этот параметр определяет первое.";
            fractionalDigits = 2;
            tags.Add(ParameterTag.items);
        }
    }
}
