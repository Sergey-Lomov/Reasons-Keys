using System;

namespace ModelAnalyzer.Services
{
    class Dichotomiser
    {
        public double epsilon = 0.01F;
        //       float iterationLimit = 1000;
        public Func<double, double> func;
        public double left, right;

        public double calculate()
        {
            double cLeft = left;
            double cRight = right;
            if (func(left) * func(right) >= 0)
            {
                Console.WriteLine("You have not assumed right left and right");
                return 0;
            }

            double center = cLeft;
            while (Math.Abs(cRight - cLeft) >= epsilon)
            {
                center = (cLeft + cRight) / 2;
                if (func(center) == 0.0)
                    break;

                else if (func(center) * func(cLeft) < 0)
                    cRight = center;
                else
                    cLeft = center;
            }
            
            return center;
        }
    }
}
