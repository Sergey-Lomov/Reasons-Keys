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
        readonly string invalidInMessage = "Для вычисления необходимы параметры: {0}";

        public abstract void SetupByString(string str);
        public abstract string StringRepresentation();

        internal virtual Parameter Copy ()
        {
            var selfType = GetType();
            var copy = Activator.CreateInstance(selfType) as Parameter;

            copy.type = type;
            copy.title = title;
            copy.details = details;

            copy.tags.Clear();
            copy.tags.AddRange(tags);

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

        internal virtual ParameterCalculationReport Calculate(Calculator calculator)
        {
            return calculationReport;
        }

        internal virtual ParameterValidationReport Validate(Validator validator, Storage storage)
        {
            return new ParameterValidationReport(this);
        }

        internal void FailCalculationByInvalidIn(string[] parametersTitles)
        {
            string titles = "";
            foreach (string title in parametersTitles)
                titles += "\"" + title + "\",";
            titles.Remove(titles.Length - 1);

            string issue = string.Format(invalidInMessage, titles);
            calculationReport.Failed(issue);
        }
    }
}