using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelAnalyzer.Parameters.Events
{
    class AverageStabilityIncrement : SingleParameter
    {
        public AverageStabilityIncrement()
        {
            type = ParameterType.In;
            title = "Средний прирост стабильности события";
            details = "";
            fractionalDigits = 2;
        }
    }
}
