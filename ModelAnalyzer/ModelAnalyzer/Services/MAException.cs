using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelAnalyzer
{
    class MAException : SystemException
    {
        string message;
        Parameter parameter;

        public MAException(string message)
        : base(message)
        {
            this.message = message;
        }

        public MAException(string message, Parameter parameter)
        : base(string.Format("Parametr: {0}\nMessage: {1}", parameter.title, message))
        {
            this.message = message;
            this.parameter = parameter;
        }
    }
}
