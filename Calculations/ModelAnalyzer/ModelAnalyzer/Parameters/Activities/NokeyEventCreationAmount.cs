﻿namespace ModelAnalyzer.Parameters.Activities
{
    class NokeyEventCreationAmount : FloatSingleParameter
    {
        public NokeyEventCreationAmount()
        {
            type = ParameterType.In;
            title = "Стандартное кол-во организации не решающих событий";
            details = "Под не решающими событиями подразумеваются изначальные события и события континуума. Изначальные события, это те события, которые игроки имеют на старте игры, но при этом эти события не имеют большого количества очков ветвей. События континуума, это те события, которые располагаются на оборотной стороне поля.";
            fractionalDigits = 2;
            tags.Add(ParameterTag.activities);
        }
    }
}