using System.Collections.Generic;
namespace ModelAnalyzer.Services
{
    public abstract class OperationReport
    {
        abstract internal string operationTitle { get; }
        internal bool IsSuccess => issues.Count == 0;
        protected List<string> issues = new List<string>();

        internal List<string> GetIssues()
        {
            return new List<string>(issues);
        }

        internal void AddIssue(string issue)
        {
            if (!issues.Contains(issue))
                issues.Add(issue);
        }

        internal void AddIssues(List<string> newIssues)
        {
            foreach (var newIssue in newIssues)
                AddIssue(newIssue);
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
