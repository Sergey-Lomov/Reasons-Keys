using System.Collections.Generic;

using ModelAnalyzer.Services;

namespace ModelAnalyzer.Parameters
{
    public class ParameterOperationReport : OperationReport
    {
        public Parameter parameter;

        internal override string operationTitle => parameter.title;

        internal ParameterOperationReport(Parameter parameter)
        {
            this.parameter = parameter;
        }
    }

    public class ParameterValidationReport : ParameterOperationReport
    {
        internal ParameterValidationReport(Parameter parameter) : base(parameter) { }
    }

    public class ParameterCalculationReport : ParameterOperationReport
    {
        public Parameter precalculated;

        public bool WasChanged => !parameter.IsEqual(precalculated);

        internal ParameterCalculationReport(Parameter parameter) : base(parameter)
        {
            precalculated = parameter.Copy();
        }
    }
}
