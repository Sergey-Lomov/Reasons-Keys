using System.Linq;

using ModelAnalyzer.Services;

namespace ModelAnalyzer.Parameters
{
    abstract class FloatSingleParameter : DigitalParameter
    {
        protected float value = float.NaN;
        protected float unroundValue = float.NaN;

        internal override Parameter Copy()
        {
            var copy = base.Copy() as FloatSingleParameter;
            copy.value = value;
            copy.unroundValue = unroundValue;

            return copy;
        }

        internal override bool IsEqual(Parameter p)
        {
            if (!(p is FloatSingleParameter))
                return false;

            var baseCheck = base.IsEqual(p);

            var fsp = p as FloatSingleParameter;
            var valueCheck = fsp.value == value;
            var unroundCheck = fsp.unroundValue == unroundValue;

            return baseCheck && valueCheck && unroundCheck;
        }

        internal override ParameterValidationReport Validate(Validator validator, Storage storage)
        {
            var report = base.Validate(validator, storage);

            var roundingIssues = validator.ValidateRounding(unroundValue, value);
            report.issues.AddRange(roundingIssues);

            return report;
        }

        public override void SetupByString(string str)
        {
            string[] subs = str.Split(dataSeparator.ToCharArray());

            if (subs.Count() == 0)
            {
                string message = string.Format("Can't parse string to value and unround value: \"{0}\"", str);
                MAException e = new MAException(message, this);
                throw e;
            }

            value = FloatFromString(subs[0]);

            if (subs.Count() >= 2)
            {
                unroundValue = FloatFromString(subs[1]);
            }
            else
            {
                unroundValue = float.NaN;
            }
        }

        private float FloatFromString(string str)
        {
            try
            {
                return Utils.FloatFromString(str, invalidValueStub);
            }
            catch (MAException e)
            {
                throw new MAException(e.Message, this);
            }
        }

        public override string StringRepresentation()
        {
            return ValueToString() + dataSeparator + UnroundValueToString();
        }

        public override string ValueToString()
        {
            return Utils.FloatToString(value, fractionalDigits, invalidValueStub);
        }

        public override string UnroundValueToString()
        {
            return Utils.FloatToString(unroundValue, unroundFractionalDigits, invalidValueStub);
        }

        public float GetValue()
        {
            return value;
        }
    }
}
