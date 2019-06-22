using System;
using System.Collections.Generic;
using System.Linq;

namespace ModelAnalyzer
{
    class Storage
    {
        Dictionary<Type, Parameter> parameters = new Dictionary<Type, Parameter>();

        public void AddParameter(Parameter parameter)
        {
            parameters[parameter.GetType()] = parameter;
        }

        public Parameter GetParameter(Type type)
        {
            Parameter p = parameters[type];
            if (p == null)
            {
                String message = String.Format("Invalid parameter type: {0}", type.ToString());
                MAException e = new MAException(message);
                throw e;
            }

            return p;
        }
        
        public List<Parameter> GetParameters()
        {
            return parameters.Values.ToList();
        }

        public List<Parameter> GetParameters(ParameterType[] types, string titleFilter = null)
        {
            Func<Parameter, bool> filterLambda = p => Array.Exists(types, type => type == p.type);
            Func<Parameter, string> sortLambda = p => p.title;
            List<Parameter> result = parameters.Values.Where(filterLambda).OrderBy(sortLambda).ToList();

            if (titleFilter != null)
            {
                Func<Parameter, bool> titleFilterLambda = p => p.title.ToUpper().Contains(titleFilter.ToUpper());
                result = result.Where(titleFilterLambda).ToList();
            }

            return result;
        }

        internal float GetSingleValue(Type type)
        {
            var parameter = GetParameter(type);
            if (parameter is SingleParameter single)
                return single.GetValue();

            return 0;
        }

        internal float[] GetArrayValue(Type type)
        {
            var parameter = GetParameter(type);
            if (parameter is ArrayParameter array)
                return array.GetValue();

            return new float[] { };
        }
    }
}
