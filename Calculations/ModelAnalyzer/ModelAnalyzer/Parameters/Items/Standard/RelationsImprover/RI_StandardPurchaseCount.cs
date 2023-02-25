
namespace ModelAnalyzer.Parameters.Items.Standard.RelationsImprover
{
    class RI_StandardPurchaseCount : FloatSingleParameter
    {
        public RI_StandardPurchaseCount()
        {
            type = ParameterType.In;
            title = "УС: стандартное кол-во покупок";
            details = "В течении партии игрок может купить несколько усилителей связей. Поэтому по, аналогии со стандартными кол-вами других действий игроков, работает данный параметр.";
            fractionalDigits = 0;
            tags.Add(ParameterTag.items);
            tags.Add(ParameterTag.baseItems);
        }
    }
}
