namespace ModelAnalyzer.Parameters.Items.Standard.KineticAccumulator
{
    class KA_InversePower : SingleParameter
    {
        public KA_InversePower()
        {
            type = ParameterType.In;
            title = "Накопитель ТЗ: обратная мощность";
            details = "Указывает кол-во перемещений, необходимых для получения 1 ТЗ";
            fractionalDigits = 0;
            tags.Add(ParameterTag.items);
            tags.Add(ParameterTag.mining);
        }
    }
}
