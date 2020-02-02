namespace ModelAnalyzer.Parameters.Moving
{
    class SpeedDoublingRate : FloatSingleParameter
    {
        public SpeedDoublingRate()
        {
            type = ParameterType.In;
            title = "Оценка удвоения скорости";
            details = "Этот параметр задает связь между удвоением скорости и ТЗ. Значение указывается в средних добычах. Например если значение равно 2.5 это значит, что выгода от эффекта дающего удвоение скорости оценивается как 2.5 средних добыч ТЗ.";
            fractionalDigits = 2;
            tags.Add(ParameterTag.moving);
        }
    }
}
