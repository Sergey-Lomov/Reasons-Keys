using ModelAnalyzer.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ModelAnalyzer.Services
{
    class MathAdditional
    {
        private static readonly string roundingIssue = "Невозможно корректно округлить значения при распределении. Сумма округленных значений отличется суммы не округленных.";

        internal static double Factorial (int value)
        {
            double factorial = 1;
            for (int i = 1; i <= value; i++)
                factorial *= i;
            return factorial;
        }

        internal static double miltyply(int from, int to)
        {
            double result = from;
            for (int i = from + 1; i <= to; i++)
                result *= i;
            return result;
        }

        internal static float sum(int from, int to, Func<float, float> innerFunc)
        {
            float result = 0;
            for (int i = from; i <= to; i++)
                result += innerFunc(i);
            return result;
        }

        internal static float average(IEnumerable<int> values)
        {
            if (values.Count() > 0)
                return (float)values.Average();
            return 0;
        }

        internal static List<List<T>> combinations<T>(List<T> availableValues, 
            int length,
            List<T> initial = null)
        {
            if (initial == null)
                initial = new List<T>();

            var results = new List<List<T>>();
            if (length == 0)
            {
                return results;
            }

            foreach (var value in availableValues)
            {
                var extended = new List<T>(initial) { value };

                if (extended.Count == length)
                {
                    results.Add(extended);
                } else
                {
                    var subcombinations = combinations(availableValues, length, extended);
                    results.AddRange(subcombinations);
                }
            }

            return results;
        }

        internal static float combinationChance<T>(List<T> combination, Dictionary<T, float> valuesChances)
        {
            float chance = 1;
            foreach (var value in combination)
            {
                chance *= valuesChances[value];
            }
            return chance;
        }

        internal static double combinationsAmount(int chosen, int total)
        {
            return Factorial(total) / Factorial(chosen) / Factorial(total - chosen);
        }

        internal static T normalise<T>(T value, T min, T max) where T : IComparable<T>
        {
            var result = value.CompareTo(min) < 0 ? min : value;
            result = value.CompareTo(max) > 0 ? max : result;
            return result;
        }

        internal static int[] AmountsForAllocation(float totalAmount, List<float> allocation, ParameterCalculationReport report)
        {
            int[] amounts = new int[allocation.Count()];
            float roundCredit = 0;
            for (int i = 0; i < allocation.Count(); i++)
            {
                if (allocation[i] == 0)
                    continue;

                var amount = totalAmount * allocation[i] / allocation.Sum() + roundCredit;
                amounts[i] = (int)Math.Round(amount, MidpointRounding.AwayFromZero);
                roundCredit = amount - amounts[i];
            }

            if (amounts.Sum() != totalAmount)
            {
                report.Failed(roundingIssue);
                return new int[0];
            }

            return amounts;
        }
    }
}
