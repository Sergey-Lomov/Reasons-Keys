namespace ModelAnalyzer.Parameters.Activities
{
    class MotionAmount : SingleParameter
    {
        public MotionAmount()
        {
            type = ParameterType.In;
            title = "Стандартное кол-во перемещений";
            details = "Предполагается, что в течении партии игрок будет производить в среднем указанное кол-во перемещений.";
            fractionalDigits = 0;
            tags.Add(ParameterTag.moving);
            tags.Add(ParameterTag.activities);
        }
    }
}
