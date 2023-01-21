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
        protected string editorLabel = "";

        public bool GetValue()
        {
            return value ?? false;
        }

        public bool? GetNullableValue()
        {
            return value;
        }

        public void SetValue(bool newValue)
        {
            value = newValue;
        }

        internal override Parameter Copy()
        {
            var copy = base.Copy() as BoolParameter;
            copy.value = value;

            return copy;
        }

        internal override bool IsEqual(Parameter p)
        {
            if (!(p is BoolParameter))
                return false;

            var baseCheck = base.IsEqual(p);
            var bp = p as BoolParameter;
            var valueCheck = bp.value == value;

            return baseCheck && valueCheck;
        }

        public override bool IsValueNull()
        {
            return !value.HasValue;
        }

        public override void SetupByString(string str)
        {
            try
            {
                value = bool.Parse(str);
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

        public string EditorLabel()
        {
            return editorLabel == "" ? title : editorLabel;
        }

        protected override void NullifyValue()
        {
            value = null;
        }
    }
}
