namespace ModelAnalyzer.Parameters.Activities
{
    class EUPartyAmount : FloatSingleParameter
    {
        public EUPartyAmount()
        {
            type = ParameterType.In;
            title = "Кол-во ТЗ, используемое игроком за партию";
            details = "Оценочное значение, на котором базируется расчет средней добычи. По сути, это значение задает масштаб практически всей остальной системе.";
            fractionalDigits = 0;
            tags.Add(ParameterTag.activities);
        }
    }
}
