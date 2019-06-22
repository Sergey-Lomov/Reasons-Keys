﻿namespace ModelAnalyzer.Parameters
{
    class FieldRadius : SingleParameter
    {
        public FieldRadius()
        {
            type = ParameterType.In;
            title = "Радиус поля";
            details = "Без учета центрального тайла";
            fractionalDigits = 0;
        }
    }
}
