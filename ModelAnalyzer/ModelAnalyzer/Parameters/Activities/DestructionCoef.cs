namespace ModelAnalyzer.Parameters.Activities
{
    class DestructionCoef : SingleParameter
    {
        public DestructionCoef()
        {
            type = ParameterType.In;
            title = "Коэф. деструкции";
            details = "Показывает отношение усилий, необходимым для уравновешивания события воздействиями к усилиям вложенным в создание его стабильности. Чем больше этот коэф. тем меньше разница между усилиями игрока нарушающего причнно-следственные связи с помощью воздействия на события и усилия игрока создавшего цепочку ведущую к этому событию.";
            fractionalDigits = 2;
            tags.Add(ParameterTag.activities);
        }
    }
}
