namespace ModelAnalyzer.Parameters.Items
{
    class ProtectionActualityCoefficient : FloatSingleParameter
    {
        public ProtectionActualityCoefficient()
        {
            type = ParameterType.In;
            title = "Коэф. востребованности защиты";
            details = "Отображает то, насколько часто защита действительно нужна";
            fractionalDigits = 2;
            tags.Add(ParameterTag.items);
        }
    }
}
