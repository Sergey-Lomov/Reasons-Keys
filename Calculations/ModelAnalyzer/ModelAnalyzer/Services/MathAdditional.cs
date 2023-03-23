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

        internal static double Miltyply(int from, int to)
        {
            double result = from;
            for (int i = from + 1; i <= to; i++)
                result *= i;
            return result;
        }

        internal static float Sum(int from, int to, Func<float, float> innerFunc)
        {
            float result = 0;
            for (int i = from; i <= to; i++)
                result += innerFunc(i);
            return result;
        }

        internal static float Average(IEnumerable<int> values)
        {
            if (values.Count() > 0)
                return (float)values.Average();
            return 0;
        }

        internal static List<List<T>> Combinations<T>(List<T> availableValues, 
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
                    var subcombinations = Combinations(availableValues, length, extended);
                    results.AddRange(subcombinations);
                }
            }

            return results;
        }

        internal static float CombinationChance<T>(List<T> combination, Dictionary<T, float> valuesChances)
        {
            float chance = 1;
            foreach (var value in combination)
            {
                chance *= valuesChances[value];
            }
            return chance;
        }

        internal static double CombinationsAmount(int chosen, int total)
        {
            return Factorial(total) / Factorial(chosen) / Factorial(total - chosen);
        }

        internal static T Normalise<T>(T value, T min, T max) where T : IComparable<T>
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

        internal static Dictionary<K, List<V>> Split<K, V>(List<K> keys, List<V> values, List<float> weights)
        {
            var result = new Dictionary<K, List<V>>();
            if (keys.Count != weights.Count) {
                throw new Exception("Try to split with inconsistence keys and weights amount");
            }

            var unhandledValues = new List<V>(values);
            var totalWeight = weights.Sum();
            float roundingDeposit = 0;
            for (int i = 0; i < keys.Count(); i++)
            {
                var weight = weights[i] / totalWeight;
                var count = values.Count() * weight;
                var roundedCount = (int)Math.Round(count + roundingDeposit, MidpointRounding.AwayFromZero);
                roundingDeposit += count - roundedCount;
                result[keys[i]] = unhandledValues.Take(roundedCount).ToList();
                unhandledValues = unhandledValues.Skip(roundedCount).ToList();
            }

            return result;
        }
    }
}
