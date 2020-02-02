namespace ModelAnalyzer.Parameters.Timing
{
    class RealisationDuration : FloatSingleParameter
    {
        public RealisationDuration()
        {
            type = ParameterType.In;
            title = "Оценка времени на реализацию события (мин)";
            details = "Оценка примерная и нуждается в уточнении во время тестов";
            fractionalDigits = 2;
            tags.Add(ParameterTag.timing);
        }
    }
}
