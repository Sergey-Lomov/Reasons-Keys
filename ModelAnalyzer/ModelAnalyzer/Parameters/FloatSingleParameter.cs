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
            report.AddIssues(roundingIssues);

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

            value = FloatStringConverter.FloatFromString(subs[0]);

            if (subs.Count() >= 2)
            {
                unroundValue = FloatStringConverter.FloatFromString(subs[1]);
            }
            else
            {
                unroundValue = float.NaN;
            }
        }

        public override string StringRepresentation()
        {
            return FloatStringConverter.FloatToString(value, fractionalDigits) 
                + dataSeparator 
                + FloatStringConverter.FloatToString(unroundValue, fractionalDigits);
        }

        public float GetValue()
        {
            return value;
        }

        public float GetUnroundValue()
        {
            return unroundValue;
        }

        public void SetValue(float newValue)
        {
            value = unroundValue = newValue;
        }

        protected override void NullifyValue()
        {
            value = unroundValue = float.NaN;
        }

        internal override bool VerifyValue()
        {
            bool baseVerify = base.VerifyValue();

            return baseVerify && !float.IsNaN(value);
        }
    }
}
