namespace ModelAnalyzer.Parameters.Activities
{
    class DestructionCoef : FloatSingleParameter
    {
        public DestructionCoef()
        {
            type = ParameterType.In;
            title = "Коэф. деструкции";
            details = "Задает отношение усилий вложенных в создание стабильности среднестатистического события (его организация) к усилиям, необходимых для уравновешивания этой стабильности воздействиями.";
            fractionalDigits = 2;
            tags.Add(ParameterTag.activities);
        }
    }
}
