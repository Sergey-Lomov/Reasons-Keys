namespace ModelAnalyzer.Parameters.Items.Artifacts.CollectorModule
{
    class CM_UsageDifficulty : FloatSingleParameter
    {
        public CM_UsageDifficulty()
        {
            type = ParameterType.In;
            title = "МК: сложноть использования";
            details = "Отражает кол-во бонусов добычи (в средних бонусах добычи), которые должны быть вокруг игрока в момент использования, чтобы игрок получил выгоду от артефакта равную значению параметра “Расчетная выгодность артефактов”. Чем этот параметр выше, тем сложнее успешно использовать артефакт.";
            fractionalDigits = 2;
            tags.Add(ParameterTag.items);
            tags.Add(ParameterTag.artifacts);
        }
    }
}
