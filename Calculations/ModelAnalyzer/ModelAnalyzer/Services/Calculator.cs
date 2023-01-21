using System;
using System.Collections.Generic;

using ModelAnalyzer.Parameters;

namespace ModelAnalyzer.Services
{
    class ModelCalcultaionReport {
        public List<OperationReport> operations = new List<OperationReport>();
        public double duration;
    }

    class Calculator
    {
        private const string emptyInParametersException = "Часть входящих параметров не заполнена";

        Storage storage;
        HashSet<Parameter> updated;
        ModelCalcultaionReport modelCalculationReport;

        private readonly Dictionary<Type, CalculationModule> modules = new Dictionary<Type, CalculationModule>();
        private readonly TimingsStack timingStack = new TimingsStack();

        internal ModelCalcultaionReport CalculateModel (Storage storage)
        {
            modelCalculationReport = new ModelCalcultaionReport();
            this.storage = storage;
            updated = new HashSet<Parameter>();
            var start = DateTime.Now;

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
            
            var finish = DateTime.Now;
            var duration = (finish - start).TotalMilliseconds;
            modelCalculationReport.duration = duration;

            return modelCalculationReport;
        }

        internal T UpdatedModule<T> () where T : CalculationModule, new()
        {
            if (modules.ContainsKey(typeof(T)))
                return modules[typeof(T)] as T;

            var module = new T();
            timingStack.StartNewTiming();
            var report = module.Execute(this);
            report.duration = timingStack.FinishCurrentTiming();
            modelCalculationReport.operations.Add(report);
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
                timingStack.StartNewTiming();
                var report = parameter.Calculate(this);
                report.duration = timingStack.FinishCurrentTiming();
                modelCalculationReport.operations.Add(report);
                updated.Add(parameter);
            }
        }
    }
}
