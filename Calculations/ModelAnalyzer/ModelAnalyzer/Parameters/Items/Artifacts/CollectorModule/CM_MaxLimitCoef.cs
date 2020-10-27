namespace ModelAnalyzer.Parameters.Items.Artifacts.CollectorModule
{
    class CM_MaxLimitCoef : FloatSingleParameter
    {
        public CM_MaxLimitCoef()
        {
            type = ParameterType.In;
            title = "МК: коэф. максимального лимита";
            details = "Задает связь между расчетной выгондостью артефакта и верхним пределом бонуса добычи, который МК может дать игроку";
            fractionalDigits = 2;
            tags.Add(ParameterTag.items);
            tags.Add(ParameterTag.artifacts);
        }
    }
}
