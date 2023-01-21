using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelAnalyzer.Parameters
{
    class BoolParameter : Parameter
    {
        readonly private string nullValueStub = "-";

        protected bool? value = null;

        public override bool IsValueNull()
        {
            return !value.HasValue;
        }

        public override void SetupByString(string str)
        {
            try
            {
                value = bool.Parse(str)
            }
            catch
            {
                value = null;
            }
        }

        public override string StringRepresentation()
        {
            return value.HasValue ? value.ToString() : nullValueStub;
        }

        protected override void NullifyValue()
        {
            value = null;
        }
    }
}
