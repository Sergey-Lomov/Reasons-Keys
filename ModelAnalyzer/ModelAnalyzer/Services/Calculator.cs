using System;
using System.Collections.Generic;

namespace ModelAnalyzer
{
    using ModelCalcultaionReport = List<ParameterCalculationReport>;

    class Calculator
    {
        Storage storage;
        HashSet<Parameter> updated;
        ModelCalcultaionReport modelCalculationReport;

        internal ModelCalcultaionReport CalculateModel (Storage storage)
        {
            modelCalculationReport = new ModelCalcultaionReport();
            this.storage = storage;
            updated = new HashSet<Parameter>();

            ParameterType[] filter = new ParameterType[] { ParameterType.Out, ParameterType.Inner, ParameterType.Indicator };
            List<Parameter> parameters = storage.Parameters(filter);

            foreach (Parameter parameter in parameters)
                CalculateIfNecessary(parameter);

            return modelCalculationReport;
        }

        internal float UpdatedSingleValue(Type type)
        {
            var parameter = storage.Parameter(type);
            CalculateIfNecessary(parameter);
            return storage.SingleValue(type);
        }

        internal float[] UpdatedArrayValue(Type type)
        {
            var parameter = storage.Parameter(type);
            CalculateIfNecessary(parameter);
            return storage.ArrayValue(type);
        }

        internal T UpdatedParameter<T>() where T : Parameter
        {
            var parameter = storage.Parameter(typeof(T));
            CalculateIfNecessary(parameter);
            return parameter as T;
        }

        internal string ParameterTitle(Type type)
        {
            return storage.Parameter(type).title;
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
