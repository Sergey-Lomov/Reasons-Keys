using System;
using System.Collections.Generic;
using System.Linq;

namespace ModelAnalyzer.Services
{
    static class Extensions
    {
        internal static float Deviation(this IEnumerable<float> collection)
        {
            return collection.Max(x => Math.Abs(collection.Average() - x));
        }

        internal static string HumanRedeable(this bool? value)
        {
            if (!value.HasValue) {
                return "Не определено";
            } else
            {
                return value.Value ? "Да" : "Нет";
            }
        }

        internal static string ToIssuesList(this List<string> list, string itemPrefix)
        {
            var result = "";
            foreach (string issue in list)
            {
                var linePrefix = list.Count > 1 ? itemPrefix : "";
                result += linePrefix + issue;
                if (issue != list.Last())
                    result += Environment.NewLine;
            }
            return result;
        }
    }
}
