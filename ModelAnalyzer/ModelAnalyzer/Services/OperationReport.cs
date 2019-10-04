using System.Collections.Generic;
namespace ModelAnalyzer.Services
{
    public abstract class OperationReport
    {
        abstract internal string operationTitle { get; }
        internal bool IsSuccess => issues.Count == 0;
        internal List<string> issues = new List<string>();

        internal void AddIssue(string issue)
        {
            issues.Add(issue);
        }

        internal void Failed(string issue)
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
