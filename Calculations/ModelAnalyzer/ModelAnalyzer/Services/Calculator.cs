using System;
using System.Collections.Generic;

using ModelAnalyzer.Parameters;

namespace ModelAnalyzer.Services
{
    using ModelCalcultaionReport = List<OperationReport>;

    class Calculator
    {
        Storage storage;
        HashSet<Parameter> updated;
        ModelCalcultaionReport modelCalculationReport;

        private Dictionary<Type, CalculationModule> modules = new Dictionary<Type, CalculationModule>();

        internal ModelCalcultaionReport CalculateModel (Storage storage)
        {
            modelCalculationReport = new ModelCalcultaionReport();
            this.storage = storage;
            updated = new HashSet<Parameter>();

            ParameterType[] filter = new ParameterType[] { ParameterType.Out, ParameterType.Inner, ParameterType.Indicator };
            List<Parameter> parameters = storage.Parameters(filter);

            foreach (Parameter parameter in parameters)
                CalculateIfNecessary(parameter);

            modules.Clear();

            return modelCalculationReport;
        }

        internal T UpdatedModule<T> () where T : CalculationModule, new()
        {
            if (modules.ContainsKey(typeof(T)))
                return modules[typeof(T)] as T;

            var module = new T();
            var report = module.Execute(this);
            modelCalculationReport.Add(report);
            modules[typeof(T)] = module;
            return module;
        }

        internal T UpdatedParameter<T>() where T : Parameter
        {
            var parameter = storage.Parameter<T>();
            CalculateIfNecessary(parameter);
            return parameter as T;
        }

        private void CalculateIfNecessary (Parameter parameter)
        {
            if (!updated.Contains(parameter) && parameter.type != ParameterType.In)
            {
                var report = parameter.Calculate(this);
                modelCalculationReport.Add(report);
                updated.Add(parameter);
            }
        }
    }
}
