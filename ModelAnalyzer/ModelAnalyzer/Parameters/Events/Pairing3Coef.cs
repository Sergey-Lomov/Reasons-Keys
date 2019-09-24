namespace ModelAnalyzer.Parameters.Events
{
    class Pairing3Coef : SingleParameter
    {
        public Pairing3Coef()
        {
            type = ParameterType.In;
            title = "Коэф. тройного сопряжения";
            details = "Задает отношение между кол-вом карт с тройным сопряжением и кол-вом карт, которые могли бы его иметь.";
            fractionalDigits = 2;
            tags.Add(ParameterTag.events);
        }
    }
}
