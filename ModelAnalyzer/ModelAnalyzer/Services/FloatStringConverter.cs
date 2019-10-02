using System.Collections.Generic;

namespace ModelAnalyzer.Services
{
    class FloatStringConverter
    {
        private readonly static string separator = " ";
        private readonly static string invalidStub = "-";

        internal static string ListToString(List<float> list, int fractionalDigits)
        {
            string result = "";
            for (int i = 0; i < list.Count; i++)
            {
                result += FloatToString(list[i], fractionalDigits);
                string currentSeparator = i < list.Count - 1 ? separator : "";
                result += currentSeparator;
            }

            return result;
        }

        internal static List<float> ListFromString(string str)
        {
            List<float> list = new List<float>();
            if (str.Length == 0)
                return list;

            string[] subs = str.Split(separator.ToCharArray());
            foreach (string sub in subs)
                list.Add(FloatFromString(sub));

            return list;
        }

        internal static string FloatToString(float value, int fractionalDigits)
        {
            if (float.IsNaN(value))
                return invalidStub;

            string format = FormatForFractional(fractionalDigits);
            return string.Format(format, value);
        }

        internal static float FloatFromString(string str)
        {
            float value;
            if (str == invalidStub)
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

        private static string FormatForFractional(int fractionalDigits)
        {
            string format = "{0:0.";
            for (int i = 0; i < fractionalDigits; i++)
                format += "#";
            format += "}";

            return format;
        }
    }
}
