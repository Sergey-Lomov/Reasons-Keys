using System;
using System.Linq;
using System.Collections.Generic;

using ModelAnalyzer.Services;

namespace ModelAnalyzer.Parameters
{
    public enum ParameterType {In, Out, Inner, Indicator}

    public abstract class Parameter
    {
        public ParameterType type;
        internal ParameterCalculationReport calculationReport = null;

        public string title;
        public string details;
        public List<ParameterTag> tags = new List<ParameterTag>();
        readonly protected string dataSeparator = "~";
        readonly string multyInvalidInMessage = "Для вычисления необходимы параметры: {0}";
        readonly string singleInvalidInMessage = "Для вычисления необходим параметр: \"{0}\""; 

        public abstract void SetupByString(string str);
        public abstract string StringRepresentation();

        public abstract bool isValueNull();
        protected abstract void NullifyValue();

        internal virtual Parameter Copy ()
        {
            var selfType = GetType();
            var copy = Activator.CreateInstance(selfType) as Parameter;

            copy.type = type;
            copy.title = title;
            copy.details = details;

            copy.tags.Clear();
            copy.tags.AddRange(tags);

            if (calculationReport != null)
                copy.calculationReport = new ParameterCalculationReport(calculationReport);

            return copy;
        }

        internal virtual bool IsEqual(Parameter p)
        {
            var typeCheck = p.type == type;
            var titleCheck = p.title.Equals(title);
            var detailsCheck = p.details.Equals(details);
            var tagsCheck = p.tags.SequenceEqual(tags);

            return typeCheck && titleCheck && detailsCheck && tagsCheck;
        }

        // Return true if value may be used in other calculations
        internal virtual bool VerifyValue ()
        {
            if (type == ParameterType.In)
                return !isValueNull();

            if (calculationReport != null)
                return calculationReport.IsSuccess;
            else
                return false;
        }

        internal virtual ParameterCalculationReport Calculate(Calculator calculator)
        {
            return calculationReport;
        }

        internal virtual ParameterValidationReport Validate(Validator validator, Storage storage)
        {
            return new ParameterValidationReport(this);
        }

        internal void FailCalculationByInvalidIn(string parameterTitle)
        {
            string issue = string.Format(singleInvalidInMessage, parameterTitle);
            calculationReport.AddIssue(issue);
        }

        internal void FailCalculationByInvalidIn(string[] parametersTitles)
        {
            string titles = "";
            foreach (string title in parametersTitles)
                titles += "\"" + title + "\",";
            titles.Remove(titles.Length - 1);

            string issue = string.Format(multyInvalidInMessage, titles);
            calculationReport.AddIssue(issue);
        }

        internal T RequestParmeter<T> (Calculator calculator) where T : Parameter
        {
            var parameter = calculator.UpdatedParameter<T>();

            if (!parameter.VerifyValue())
            {
                NullifyValue();
                FailCalculationByInvalidIn(parameter.title);
            }

            return parameter;
        }
    }
}