using System;

using ModelAnalyzer.Parameters;

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

    class MACalculationException : MAException
    {
        public MACalculationException(string message) : base(message)
        {
        }
    }
}
