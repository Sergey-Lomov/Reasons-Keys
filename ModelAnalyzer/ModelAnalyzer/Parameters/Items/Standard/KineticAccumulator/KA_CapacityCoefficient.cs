namespace ModelAnalyzer.Parameters.Items.Standard.KineticAccumulator
{
    class KA_CapacityCoefficient : SingleParameter
    {
        public KA_CapacityCoefficient()
        {
            type = ParameterType.In;
            title = "Накопитель ТЗ: коэф. объема";
            details = "Управляет объемом накопителя (подробнее см. документ по механике базовых предметов)";
            fractionalDigits = 2;
            tags.Add(ParameterTag.mining);
            tags.Add(ParameterTag.items);
        }
    }
}
