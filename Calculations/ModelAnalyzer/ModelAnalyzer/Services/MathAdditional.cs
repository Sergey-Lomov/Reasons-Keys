using System;

namespace ModelAnalyzer.Services
{
    class MathAdditional
    {
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

        internal static double sum(int from, int to, Func<float, double> innerFunc)
        {
            double result = 0;
            for (int i = from; i <= to; i++)
                result += innerFunc((float)i);
            return result;
        }

        internal static double combination(int chosen, int total)
        {
            return Factorial(total) / Factorial(chosen) / Factorial(total - chosen);
        }
    }
}
