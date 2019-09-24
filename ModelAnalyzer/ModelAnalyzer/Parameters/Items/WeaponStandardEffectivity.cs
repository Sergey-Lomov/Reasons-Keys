namespace ModelAnalyzer.Parameters.Items
{
    class WeaponStandardEffectivity : SingleParameter
    {
        public WeaponStandardEffectivity()
        {
            type = ParameterType.In;
            title = "Оружие: стандартная эффективность";
            details = "Точкой отсчета при расчетах связанных с оружие являются абстрактные “стандартные” значения мощности и эффективности. Этот параметр определяет второе.";
            fractionalDigits = 2;
            tags.Add(ParameterTag.items);
        }
    }
}
