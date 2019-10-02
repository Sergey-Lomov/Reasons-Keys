namespace ModelAnalyzer.Parameters.Items.Artifacts.HoleBox
{
    class HB_TensionInreasingStepsAmount : FloatSingleParameter
    {
        public HB_TensionInreasingStepsAmount()
        {
            type = ParameterType.In;
            title = "ДК: кол-во ступеней прироста напряженности";
            details = "Входящий параметр, задающий кол-во делений на шкале прироста, не считая деление соответствующее нулевому антизаряду";
            fractionalDigits = 0;
            tags.Add(ParameterTag.items);
            tags.Add(ParameterTag.artifacts);
        }
    }
}
