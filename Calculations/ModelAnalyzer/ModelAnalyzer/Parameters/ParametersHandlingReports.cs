using System.Collections.Generic;

using ModelAnalyzer.Services;

namespace ModelAnalyzer.Parameters
{
    public class ParameterOperationReport : OperationReport
    {
        public Parameter parameter;

        internal override string OperationTitle => parameter.title;

        internal ParameterOperationReport(ParameterOperationReport report) : base(report)
        {
            parameter = report.parameter;
        }

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

        internal ParameterCalculationReport(ParameterCalculationReport report) : base(report)
        {
            precalculated = report.precalculated;
        }

        internal ParameterCalculationReport(Parameter parameter) : base(parameter)
        {
            precalculated = parameter.Copy();

            if (precalculated.calculationReport != null)
                precalculated.calculationReport.precalculated = null;
        }
    }
}
