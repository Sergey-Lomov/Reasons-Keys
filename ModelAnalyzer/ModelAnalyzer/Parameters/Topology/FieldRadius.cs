namespace ModelAnalyzer.Parameters.Topology
{
    class FieldRadius : SingleParameter
    {
        public FieldRadius()
        {
            type = ParameterType.In;
            title = "Радиус поля";
            details = "Без учета центрального тайла";
            fractionalDigits = 0;
            tags.Add(ParameterTag.topology);
        }
    }
}
