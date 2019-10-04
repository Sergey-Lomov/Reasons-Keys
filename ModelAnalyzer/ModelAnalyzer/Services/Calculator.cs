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

        internal CalculationModule UpdatedModule (Type type)
        {
            if (modules.ContainsKey(type))
                return modules[type];

            var instance = Activator.CreateInstance(type);
            if (instance is CalculationModule)
            {
                var module = instance as CalculationModule;
                var report = module.Execute(this);
                modelCalculationReport.Add(report);
                modules[type] = module;
                return module;
            }

            return null;
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
