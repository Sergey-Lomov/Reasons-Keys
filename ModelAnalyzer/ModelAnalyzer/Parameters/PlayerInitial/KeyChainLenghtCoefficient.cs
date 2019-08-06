namespace ModelAnalyzer.Parameters.PlayerInitial
{
    class KeyChainLenghtCoefficient : SingleParameter
    {
        public KeyChainLenghtCoefficient()
        {
            type = ParameterType.In;
            title = "Коэф. длины решающей цепи";
            details = "Этот коэффициенты определяют отношение кол-ва событий в цепочке (достаточно стабильной для подключения к ней решающего события) к параметру “Стандартное кол-во организации не решающих событий”";
            fractionalDigits = 2;
        }
    }
}
