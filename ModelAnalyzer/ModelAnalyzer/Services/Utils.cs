using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelAnalyzer
{
    class Utils
    {
        public static string FloatToString(float value, int fractionalDigits, string invalidStub)
        {
            if (float.IsNaN(value))
                return invalidStub;

            string format = Utils.FormatForFractional(fractionalDigits);
            return string.Format(format, value);
        }

        public static string FormatForFractional (int fractionalDigits)
        {
            string format = "{0:0.";
            for (int i = 0; i<fractionalDigits; i++)
                format += "#";
            format += "}";

            return format;
        }

        public static float FloatFromString(string str, string invalidValueStub)
        {
            float value;
            if (str == invalidValueStub)
            {
                value = float.NaN;
            }
            else
            {
                if (!float.TryParse(str, out value))
                {
                    string message = string.Format("Can't parse string to float: \"{0}\"", str);
                    MAException e = new MAException(message);
                    throw e;
                }
            }

            return value;
        }
    }
}
