namespace ModelAnalyzer.Parameters.Items.Artifacts.CollectorModule
{
    class CM_MinLimitCoef : FloatSingleParameter
    {
        public CM_MinLimitCoef()
        {
            type = ParameterType.In;
            title = "МК: коэф. минимально лимита";
            details = "Задает связь между расчетной выгондостью артефакта и нижним пределом бонуса добычи, который МК гарантированно дает игроку";
            fractionalDigits = 2;
            tags.Add(ParameterTag.items);
            tags.Add(ParameterTag.artifacts);
        }
    }
}
