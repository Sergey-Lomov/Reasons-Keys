namespace ModelAnalyzer.Services
{
    internal class ModuleCalculationReport : OperationReport
    {
        internal string moduleTitle;
        internal override string operationTitle => moduleTitle;

        internal ModuleCalculationReport(CalculationModule module)
        {
            moduleTitle = module.title;
        }
    }

    internal abstract class CalculationModule
    {
        internal string title;

        abstract internal ModuleCalculationReport Execute(Calculator calculator);
    }
}
