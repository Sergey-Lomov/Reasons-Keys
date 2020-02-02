namespace ModelAnalyzer.Services
{
    class MathAdditional
    {
        internal static int Factorial (int value)
        {
            var factorial = 1;
            for (int i = 1; i <= value; i++)
                factorial *= i;
            return factorial;
        }
    }
}
