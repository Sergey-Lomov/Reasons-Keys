namespace ModelAnalyzer.Parameters.Items
{
    class PureEUProfitCoefficient : SingleParameter
    {
        public PureEUProfitCoefficient()
        {
            type = ParameterType.In;
            title = "Коэф. выгодности чистого ТЗ";
            details = "Как правило выгодность предмета оценивается в 1 ТЗ, если он позволяет игроку в какой-либо ситуации сэкономить 1 ТЗ. Если же игрок получает непосредственно 1 ТЗ в своей распоряжение это явно значительно лучше. Этот коэффициент определяет, за скоьлко ТЗ выгодности считается получение 1 ТЗ. Подробнее в документе о механике предметов.";
            fractionalDigits = 2;
            tags.Add(ParameterTag.items);
        }
    }
}
