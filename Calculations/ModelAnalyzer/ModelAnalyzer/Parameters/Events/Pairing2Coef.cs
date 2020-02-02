﻿namespace ModelAnalyzer.Parameters.Events
{
    class Pairing2Coef : FloatSingleParameter
    {
        public Pairing2Coef()
        {
            type = ParameterType.In;
            title = "Коэф. двойного сопряжения";
            details = "Задает отношение между кол-вом карт с двойным сопряжением и кол-вом карт, которые могли бы его иметь. При этом важно учесть, что карты, имеющие 3 причины не считаются картами, которые могут иметь двойное сопряжение. Потому-что согласно правилам, если на карте есть двойное сопряжение и простая причина, она игнорируется, а следовательно такая карта полностью игнорируется.";
            fractionalDigits = 2;
            tags.Add(ParameterTag.events);
        }
    }
}
