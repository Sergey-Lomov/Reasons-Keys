using System;
using System.Collections.Generic;
using System.Linq;

using ModelAnalyzer.Services.FieldAnalyzer;

namespace ModelAnalyzer.Parameters 
{
    class FieldNodesParameter : Parameter
    {
        const string stringRepresentationStub = "Карта поля";

        internal Dictionary<FieldPoint, float> field = null;
        internal int fractionalDigits = 2;

        public override void SetupByString(string str)
        {
            // Not possible. This parameter should be calculated.
        }

        public override string StringRepresentation()
        {
            return stringRepresentationStub;
        }

        public override bool isValueNull()
        {
            return field == null;
        }

        protected override void NullifyValue()
        {
            field = null;
        }
    }
}
