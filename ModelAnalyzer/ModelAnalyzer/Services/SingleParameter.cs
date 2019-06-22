using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelAnalyzer
{
    abstract class SingleParameter : Parameter
    {
        protected float value = float.NaN;
        protected float unroundValue = float.NaN;

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
