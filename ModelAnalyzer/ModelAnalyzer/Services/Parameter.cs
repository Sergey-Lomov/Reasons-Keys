using System.Collections.Generic;

namespace ModelAnalyzer
{
    public enum ParameterType {In, Out, Inner, Indicator}

    public abstract class Parameter
    {
        public ParameterType type;
        internal ParameterCalculationReport calculationReport = null;

        public string title;
        public string details;
        public List<ParameterTag> tags = new List<ParameterTag>();
        public int fractionalDigits;

        readonly protected int unroundFractionalDigits = 3;
        readonly protected string invalidValueStub = "-";
        readonly protected string dataSeparator = "~";
        readonly string invalidInMessage = "Для вычисления необходимы параметры: {0}";

        public abstract void SetupByString(string str);
        public abstract string StringRepresentation();
        public abstract string ValueToString();
        public abstract string UnroundValueToString();

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