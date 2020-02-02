using ModelAnalyzer.Services;

using System.Collections.Generic;
using System.Linq;

namespace ModelAnalyzer.Parameters
{
    abstract class FloatArrayParameter : DigitalParameter
    {
        protected List<float> values = null;
        protected List<float> unroundValues = null;

        const string emptyMessage = "Пустой массив";
        const string invalidMessage = "Массив не валиден";

        private readonly string arraySizeParamMessage = "Размер массива должен быть равен \"{0}\": {1}.";
        private readonly string arraySizeMessage = "Размер массива должен быть равен {0}";

        internal override Parameter Copy ()
        {
            var copy = base.Copy() as FloatArrayParameter;

            if (values != null)
            {
                copy.values = new List<float>();
                copy.values.AddRange(values);
            }


            if (unroundValues != null)
            {
                copy.unroundValues = new List<float>();
                copy.unroundValues.AddRange(unroundValues);
            }

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

            values = FloatStringConverter.ListFromString(subs[0]);
                
            if (subs.Count() >= 2)
            {
                unroundValues = FloatStringConverter.ListFromString(subs[1]);
            } else
            {
                unroundValues = Enumerable.Repeat(float.NaN, values.Count).ToList();
            } 
        }

        public override string StringRepresentation()
        {
            return FloatStringConverter.ListToString(values, fractionalDigits) 
                + dataSeparator 
                + FloatStringConverter.ListToString(unroundValues, fractionalDigits);
        }

        public List<float> GetValue()
        {
            return values;
        }

        public List<float> GetUnroundValue()
        {
            return unroundValues;
        }

        public void SetValue(List<float> newValues)
        {
            values = new List<float>();
            unroundValues = new List<float>();

            unroundValues.AddRange(newValues);
            values.AddRange(newValues);
        }

        internal override ParameterValidationReport Validate(Validator validator, Storage storage)
        {
            var report = base.Validate(validator, storage);
            if (values == null || unroundValues == null)
            {
                report.Failed(invalidMessage);
                return report;
            }

            for (int i = 0; i < values.Count; i++)
            {
                var roundingIssues = validator.ValidateRounding(unroundValues[i], values[i]);
                report.AddIssues(roundingIssues);
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
            if (values == null)
            {
                report.AddIssue(invalidMessage); ;
            } else if (size != values.Count)
            {
                report.AddIssue(issueMessage);
            }
        }

        protected override void NullifyValue()
        {
            values = unroundValues = null;
        }

        internal override bool VerifyValue()
        {
            bool baseVerify = base.VerifyValue();

            return baseVerify && values != null;
        }

        internal void ClearValues ()
        {
            values = new List<float>();
            unroundValues = new List<float>();
        }
    }
}
