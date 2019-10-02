using ModelAnalyzer.Services;

using System.Collections.Generic;
using System.Linq;

namespace ModelAnalyzer.Parameters
{
    abstract class FloatArrayParameter : DigitalParameter
    {
        protected List<float> values = new List<float>();
        protected List<float> unroundValues = new List<float>();

        const string separator = " ";
        const string emptyMessage = "Пустой массив";

        private readonly string arraySizeParamMessage = "Размер массива должен быть равен \"{0}\": {1}.";
        private readonly string arraySizeMessage = "Размер массива должен быть равен {0}";

        internal override Parameter Copy ()
        {
            var copy = base.Copy() as FloatArrayParameter;
            copy.values.AddRange(this.values);
            copy.unroundValues.AddRange(this.unroundValues);

            return copy;
        }

        internal override bool IsEqual(Parameter p)
        {
            if (!(p is FloatArrayParameter))
                return false;

            var baseCheck = base.IsEqual(p);

            var fsp = p as FloatArrayParameter;
            var valuesCheck = fsp.values.SequenceEqual(values);
            var unroundCheck = fsp.unroundValues.SequenceEqual(unroundValues);

            return baseCheck && valuesCheck && unroundCheck;
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

            values = ListFromString(subs[0]);
                
            if (subs.Count() >= 2)
            {
                unroundValues = ListFromString(subs[1]);
            } else
            {
                unroundValues = Enumerable.Repeat(float.NaN, values.Count).ToList();
            } 
        }

        public override string StringRepresentation()
        {
            return ListToString(values, fractionalDigits) + dataSeparator + ListToString(unroundValues, unroundFractionalDigits);
        }

        private List<float> ListFromString(string str)
        {
            List<float> list = new List<float>();
            if (str.Length == 0)
                return list;

            string[] subs = str.Split(separator.ToCharArray());
            foreach (string sub in subs)
                list.Add(FloatFromString(sub));

            return list;
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

        private string ListToString(List<float> list, int fractionalDigits)
        {
            string result = "";
            for (int i = 0; i < list.Count; i++)
            {
                result += Utils.FloatToString(list[i], fractionalDigits, invalidValueStub);
                string separator = i < list.Count - 1 ? FloatArrayParameter.separator : "";
                result += separator;
            }

            return result;
        }

        private string ListToHRString(List<float> list, int fractionalDigits)
        {
            var str = ListToString(list, fractionalDigits);
            return str.Length > 0 ? str : emptyMessage;
        }

        public override string ValueToString()
        {
            return ListToHRString(values, fractionalDigits);
        }

        public override string UnroundValueToString()
        {
            return ListToHRString(unroundValues, unroundFractionalDigits);
        }

        public float[] GetValue()
        {
            return values.ToArray();
        }

        internal override ParameterValidationReport Validate(Validator validator, Storage storage)
        {
            var report = base.Validate(validator, storage);

            for (int i = 0; i < values.Count; i++)
            {
                var roundingIssues = validator.ValidateRounding(unroundValues[i], values[i]);
                report.issues.AddRange(roundingIssues);
            }

            return report;
        }

        protected void ValidateSize(Parameter sizeParameter, ParameterValidationReport report)
        {
            if (sizeParameter is FloatSingleParameter single)
            {
                float size = single.GetValue();
                var issue = string.Format(arraySizeParamMessage, sizeParameter.title, size);
                ValidateSize(size, issue, report);
            }
        }

        protected void ValidateSize(float size, ParameterValidationReport report)
        {
            var issue = string.Format(arraySizeMessage, size);
            ValidateSize(size, issue, report);
        }

        protected void ValidateSize (float size, string issueMessage, ParameterValidationReport report)
        {
            if (size != values.Count)
            {
                report.issues.Add(issueMessage);
            }
        }
    }
}
