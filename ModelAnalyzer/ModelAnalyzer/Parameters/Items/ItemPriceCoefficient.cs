namespace ModelAnalyzer.Parameters.Items
{
    class ItemPriceCoefficient : SingleParameter
    {
        public ItemPriceCoefficient()
        {
            type = ParameterType.In;
            title = "Коэф. стоимости предметов";
            details = "После оценки выгодности каждого предмета, его полная стоимость должна быть получена умножением выгодности на 'njn rj'aabwbtyn/";
            fractionalDigits = 2;
            tags.Add(ParameterTag.items);
        }
    }
}
