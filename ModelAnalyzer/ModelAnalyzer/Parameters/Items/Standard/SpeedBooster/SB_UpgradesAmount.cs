namespace ModelAnalyzer.Parameters.Items.Standard.SpeedBooster
{
    class SB_UpgradesAmount : FloatSingleParameter
    {
        public SB_UpgradesAmount()
        {
            type = ParameterType.In;
            title = "Ускоритель: кол-во ступеней";
            details = "Общее кол-во ступеней ускорителя. В отличает от улучшений базового оружия и щитов, 3 ступени ускорителя обозначают 3 предмета. Базовым предметом, который они улучшают является сама сфера путешествий.";
            fractionalDigits = 0;
            tags.Add(ParameterTag.items);
            tags.Add(ParameterTag.baseItems);
        }
    }
}
