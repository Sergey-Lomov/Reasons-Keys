using System;
using System.Collections.Generic;

namespace ModelAnalyzer
{
    using ModelValidationReport = List<ParameterValidationReport>;

    class Validator
    {
        ModelValidationReport modelValidationReport;
        internal float absoluteGap = 0.45F;
        internal float relativeGap = 0.25F;

        const string absoluteRoundingIssue = "Разница между округленным и расчетным значением превышает {0}: {1:0.###} => {2}";
        const string relativeRoundingIssue = "Округленное значением отличается от расчетного более чем на {0:P0}: {1:0.###} => {2}";

        internal ModelValidationReport ValidateModel(Storage storage)
        {
            modelValidationReport = new ModelValidationReport();
            List<Parameter> parameters = storage.GetParameters();

            foreach (Parameter parameter in parameters)
            {
                var report = parameter.Validate(this, storage);
                modelValidationReport.Add(report);
            }

            return modelValidationReport;
        }

        internal List<string> ValidateRounding(float source, float rounded)
        {
            var issues = new List<string>();

            if (Math.Abs(rounded - source) > absoluteGap)
            {
                string issue = string.Format(absoluteRoundingIssue, absoluteGap, source, rounded);
                issues.Add(issue);
            }

            if (Math.Abs(1 - rounded / source) > relativeGap)
            {
                string issue = string.Format(relativeRoundingIssue, relativeGap, source, rounded);
                issues.Add(issue);
            }

            return issues;
        }
    }
}
