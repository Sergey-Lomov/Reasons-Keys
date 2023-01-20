using System;
using System.Collections.Generic;
using System.Linq;

using ModelAnalyzer.Parameters;

namespace ModelAnalyzer.Services
{
    class Storage
    {
        readonly Dictionary<Type, Parameter> parameters = new Dictionary<Type, Parameter>();

        public void AddParameter(Parameter parameter)
        {
            parameters[parameter.GetType()] = parameter;
        }

        public Parameter Parameter(Type type)
        {
            Parameter p = parameters.ContainsKey(type) ? parameters[type] : null;
            if (p == null)
            {
                string message = string.Format("Invalid parameter type: {0}", type.ToString());
                MAException e = new MAException(message);
                throw e;
            }

            return p;
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
            bool typesLambda(Parameter p) => Array.Exists(types, type => type == p.type);
            bool tagsLambda(Parameter p) => tags.Intersect(p.tags).Count() > 0;
            bool filterLambda(Parameter p) => typesLambda(p) && tagsLambda(p);
            string sortLambda(Parameter p) => p.title;
            List<Parameter> result = parameters.Values.Where(filterLambda).OrderBy(sortLambda).ToList();

            if (titleFilter != null)
            {
                bool titleFilterLambda(Parameter p) => p.title.ToUpper().Contains(titleFilter.ToUpper());
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
