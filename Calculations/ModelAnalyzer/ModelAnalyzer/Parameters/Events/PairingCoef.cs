namespace ModelAnalyzer.Parameters.Events
{
    class PairingCoef : FloatSingleParameter
    {
        public PairingCoef()
        {
            type = ParameterType.In;
            title = "Коэф. сопряжения";
            details = "Задает отношение между кол-вом карт с сопряжением и общим кол-вом карт";
            fractionalDigits = 2;
            tags.Add(ParameterTag.events);
        }
    }
}
