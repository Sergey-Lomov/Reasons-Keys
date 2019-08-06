using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelAnalyzer
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

        public bool IsSucces => issues.Count == 0;
        public readonly List<string> issues = new List<string>();

        public bool WasChanged => parameter.ValueToString() != oldValueString || parameter.UnroundValueToString() != oldUnroundValueString;
        public string oldValueString = null;
        public string oldUnroundValueString = null;

        internal ParameterCalculationReport(Parameter parameter)
        {
            this.parameter = parameter;
            oldValueString = parameter.ValueToString();
            oldUnroundValueString = parameter.UnroundValueToString();
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
