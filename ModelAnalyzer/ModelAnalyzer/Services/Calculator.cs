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
            List<Parameter> parameters = storage.GetParameters(filter);

            foreach (Parameter parameter in parameters)
                CalculateIfNecessary(parameter);

            return modelCalculationReport;
        }

        internal float GetUpdateSingleValue(Type type)
        {
            var parameter = storage.GetParameter(type);
            CalculateIfNecessary(parameter);
            return storage.GetSingleValue(type);
        }

        internal float[] GetUpdateArrayValue(Type type)
        {
            var parameter = storage.GetParameter(type);
            CalculateIfNecessary(parameter);
            return storage.GetArrayValue(type);
        }

        internal string GetParameterTitle(Type type)
        {
            return storage.GetParameter(type).title;
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
