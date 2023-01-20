using System;

using ModelAnalyzer.Parameters;

namespace ModelAnalyzer
{
    class MAException : SystemException
    {
#pragma warning disable IDE0052 // Remove unread private members
        readonly string message;
        readonly Parameter parameter;
#pragma warning restore IDE0052 // Remove unread private members

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
