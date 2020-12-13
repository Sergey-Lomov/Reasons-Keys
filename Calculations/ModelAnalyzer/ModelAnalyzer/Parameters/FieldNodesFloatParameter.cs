using System;

namespace ModelAnalyzer.Parameters
{
    abstract class FieldNodesFloatParameter : FieldNodesParameter<float>
    {
        internal int fractionalDigits = 2;
    }
}
