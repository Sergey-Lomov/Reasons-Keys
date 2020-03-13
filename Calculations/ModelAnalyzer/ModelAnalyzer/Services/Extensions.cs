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
    }
}
