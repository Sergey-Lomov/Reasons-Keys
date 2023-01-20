using System;
using System.Collections.Generic;

using ModelAnalyzer.Parameters;

namespace ModelAnalyzer.Services
{
    using ModelCalcultaionReport = List<OperationReport>;

    class Calculator
    {
        private const string emptyInParametersException = "Часть входящих параметров не заполнена";

        Storage storage;
        HashSet<Parameter> updated;
        ModelCalcultaionReport modelCalculationReport;

        private readonly Dictionary<Type, CalculationModule> modules = new Dictionary<Type, CalculationModule>();

        internal ModelCalcultaionReport CalculateModel (Storage storage)
        {
            modelCalculationReport = new ModelCalcultaionReport();
            this.storage = storage;
            updated = new HashSet<Parameter>();

            var inParameters = storage.Parameters(new [] { ParameterType.In});
            var missedParameters = new List<string>();
            foreach (Parameter parameter in inParameters)
                if (!parameter.VerifyValue())
                    missedParameters.Add(parameter.title);

            if (missedParameters.Count > 0)
                throw new MAException(emptyInParametersException);

            ParameterType[] filter = new ParameterType[] { ParameterType.Out, ParameterType.Inner, ParameterType.Indicator };
            List<Parameter> calculationParameters = storage.Parameters(filter);

            foreach (Parameter parameter in calculationParameters)
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
            parameter.derived++;
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
