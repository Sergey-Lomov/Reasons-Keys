namespace ModelAnalyzer.Parameters.Activities
{
    class AtackAmount : FloatSingleParameter
    {
        public AtackAmount()
        {
            type = ParameterType.In;
            title = "Стандартное кол-во нападений";
            details = "Стандартное кол-во нападений, совершаемых одним игроком на протяжении партии";
            fractionalDigits = 0;
            tags.Add(ParameterTag.activities);
        }
    }
}
