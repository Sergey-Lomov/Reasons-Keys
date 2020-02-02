using ModelAnalyzer.Parameters;

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
        readonly string invalidInMessage = "Для вычисления необходимы параметры: {0}";

        internal string title;

        abstract internal ModuleCalculationReport Execute(Calculator calculator);

        internal T RequestParmeter<T>(Calculator calculator, ModuleCalculationReport report) where T : Parameter
        {
            var parameter = calculator.UpdatedParameter<T>();

            if (!parameter.VerifyValue())
                FailCalculationByInvalidIn(parameter.title, report);

            return parameter;
        }

        internal void FailCalculationByInvalidIn(string parameterTitle, ModuleCalculationReport report)
        {
            FailCalculationByInvalidIn(new string[] { parameterTitle }, report);
        }

        internal void FailCalculationByInvalidIn(string[] parametersTitles, ModuleCalculationReport report)
        {
            string titles = "";
            foreach (string title in parametersTitles)
                titles += "\"" + title + "\",";
            titles.Remove(titles.Length - 1);

            string issue = string.Format(invalidInMessage, titles);
            report.AddIssue(issue);
        }
    }
}
