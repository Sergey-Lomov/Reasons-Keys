﻿namespace ModelAnalyzer.Parameters.Items.Standard.KineticAccumulator
{
    class KA_InversePower : FloatSingleParameter
    {
        public KA_InversePower()
        {
            type = ParameterType.In;
            title = "Накопитель ТЗ: обратная мощность";
            details = "Указывает кол-во перемещений, необходимых для получения 1 ТЗ";
            fractionalDigits = 2;
            tags.Add(ParameterTag.items);
            tags.Add(ParameterTag.baseItems);
            tags.Add(ParameterTag.mining);
        }
    }
}
