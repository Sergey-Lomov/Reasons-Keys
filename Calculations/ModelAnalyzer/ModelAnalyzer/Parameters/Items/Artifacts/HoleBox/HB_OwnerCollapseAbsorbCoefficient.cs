namespace ModelAnalyzer.Parameters.Items.Artifacts.HoleBox
{
    class HB_OwnerCollapseAbsorbCoefficient : FloatSingleParameter
    {
        public HB_OwnerCollapseAbsorbCoefficient()
        {
            type = ParameterType.In;
            title = "ДК: коэф. поглощения антизаряда владельцем при коллапсе";
            details = "Входящий параметр, обозначающий ожидаемое отношение ТЗ владельца к антизаряду артефакта в момент коллапса.";
            fractionalDigits = 2;
            tags.Add(ParameterTag.items);
            tags.Add(ParameterTag.artifacts);
        }
    }
}
