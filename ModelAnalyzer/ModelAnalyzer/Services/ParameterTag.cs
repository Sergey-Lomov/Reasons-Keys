namespace ModelAnalyzer
{
    public class ParameterTag
    {
        public string title;

        ParameterTag(string title)
        {
            this.title = title;
        }

        public override string ToString()
        {
            return title;
        }

        public static ParameterTag general = new ParameterTag("Общий");
        public static ParameterTag activities = new ParameterTag("Активность игроков");
        public static ParameterTag events = new ParameterTag("События");
        public static ParameterTag eventsWeight = new ParameterTag("Оценка веса тайла");
        public static ParameterTag mining = new ParameterTag("Добыча ТЗ");
        public static ParameterTag moving = new ParameterTag("Перемещение");
        public static ParameterTag playerInitial = new ParameterTag("Начальные состояния игроков");
        public static ParameterTag timing = new ParameterTag("Тайминг");
        public static ParameterTag topology = new ParameterTag("Топология");
        public static ParameterTag items = new ParameterTag("Предметы");
        public static ParameterTag itemStandard = new ParameterTag("Стандартные предметы");
        public static ParameterTag artifacts = new ParameterTag("Артефакты");
    }
}
