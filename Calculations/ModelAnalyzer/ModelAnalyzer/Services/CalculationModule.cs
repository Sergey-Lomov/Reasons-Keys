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
        internal int grade = 0;
        internal ModuleCalculationReport calculationReport = null;

        virtual internal ModuleCalculationReport Execute(Calculator calculator)
        {
            return calculationReport;
        }

        internal T RequestParmeter<T>(Calculator calculator) where T : Parameter
        {
            var parameter = calculator.UpdatedParameter<T>();

            if (!parameter.VerifyValue())
                FailCalculationByInvalidIn(parameter.title);

            if (grade <= parameter.grade)
                grade = parameter.grade + 1;

            return parameter;
        }

        internal void FailCalculationByInvalidIn(string parameterTitle)
        {
            FailCalculationByInvalidIn(new string[] { parameterTitle });
        }

        internal void FailCalculationByInvalidIn(string[] parametersTitles)
        {
            string titles = "";
            foreach (string title in parametersTitles)
                titles += "\"" + title + "\",";
            titles.Remove(titles.Length - 1);

            string issue = string.Format(invalidInMessage, titles);
            calculationReport.AddIssue(issue);
        }
    }
}
