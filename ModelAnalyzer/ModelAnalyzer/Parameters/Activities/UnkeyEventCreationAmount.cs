namespace ModelAnalyzer.Parameters.Activities
{
    class UnkeyEventCreationAmount : FloatSingleParameter
    {
        public UnkeyEventCreationAmount()
        {
            type = ParameterType.In;
            title = "Стандартное кол-во организации не решающих событий";
            details = "По дне решающими событиями подразумеваются события изначальные события и события континуума. Изначальные события, это те события, которые игроки имеют на старте игры, но при этом эти события не имеют большого количества очков ветвей. События континуума, это те события, которые располагаются на оборотной стороне поля.";
            fractionalDigits = 2;
            tags.Add(ParameterTag.activities);
        }
    }
}
