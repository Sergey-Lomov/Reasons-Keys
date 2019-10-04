using System;
using System.Collections.Generic;
using System.Linq;

using ModelAnalyzer.Parameters;

namespace ModelAnalyzer.Services
{
    class Storage
    {
        Dictionary<Type, Parameter> parameters = new Dictionary<Type, Parameter>();

        public void AddParameter(Parameter parameter)
        {
            parameters[parameter.GetType()] = parameter;
        }

        public T Parameter<T>() where T : Parameter
        {
            var type = typeof(T);
            Parameter p = parameters.ContainsKey(type) ? parameters[type] : null;
            if (p == null)
            {
                string message = string.Format("Invalid parameter type: {0}", type.ToString());
                MAException e = new MAException(message);
                throw e;
            }

            return p as T;
        }

        public bool HasParameterOfType(Type type)
        {
            return parameters.ContainsKey(type);
        }
        
        public List<Parameter> Parameters()
        {
            return parameters.Values.ToList();
        }

        public List<Parameter> Parameters(ParameterType[] types, string titleFilter = null)
        {
            var allTags = UniquesTags();
            return Parameters(types, allTags, titleFilter);
        }

        public List<Parameter> Parameters(ParameterType[] types, List<ParameterTag> tags, string titleFilter = null)
        {
            Func<Parameter, bool> typesLambda = p => Array.Exists(types, type => type == p.type);
            Func<Parameter, bool> tagsLambda = p => tags.Intersect(p.tags).Count() > 0;
            Func<Parameter, bool> filterLambda = p => typesLambda(p) && tagsLambda(p);
            Func<Parameter, string> sortLambda = p => p.title;
            List<Parameter> result = parameters.Values.Where(filterLambda).OrderBy(sortLambda).ToList();

            if (titleFilter != null)
            {
                Func<Parameter, bool> titleFilterLambda = p => p.title.ToUpper().Contains(titleFilter.ToUpper());
                result = result.Where(titleFilterLambda).ToList();
            }

            return result;
        }

        public List<ParameterTag> UniquesTags()
        {
            var tags = new List<ParameterTag>();

            foreach (var parameter in parameters)
                foreach (var tag in parameter.Value.tags)
                    if (!tags.Contains(tag))
                        tags.Add(tag);

            return tags;
        }
    }
}
