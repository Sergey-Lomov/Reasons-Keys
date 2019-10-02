using System.Collections.Generic;

namespace ModelAnalyzer.Parameters
{
    public class ParameterValidationReport
    {
        public Parameter parameter;
        public bool HasIssues => issues.Count > 0;
        public List<string> issues = new List<string>();

        internal ParameterValidationReport(Parameter parameter)
        {
            this.parameter = parameter;
        }
    }

    class ParameterCalculationReport
    {
        public Parameter parameter;
        private Parameter precalculated;

        public bool IsSucces => issues.Count == 0;
        public readonly List<string> issues = new List<string>();

        public bool WasChanged => !parameter.IsEqual(precalculated);

        internal ParameterCalculationReport(Parameter parameter)
        {
            this.parameter = parameter;
            precalculated = parameter.Copy();
        }

        internal void AddFailed(string issue)
        {
            issues.Add(issue);
        }

        internal void Failed (string issue)
        {
            issues.Clear();
            issues.Add(issue);
        }

        internal void Failed(List<string> issues)
        {
            issues.Clear();
            issues.AddRange(issues);
        }
    }
}
