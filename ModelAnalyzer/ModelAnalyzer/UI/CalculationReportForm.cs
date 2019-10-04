using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

using ModelAnalyzer.Services;
using ModelAnalyzer.Parameters;

namespace ModelAnalyzer.UI
{
    public partial class CalculationReportForm : Form
    {
        List<ParameterValidationReport> validations = new List<ParameterValidationReport>();
        List<ModuleCalculationReport> modulesCalculations = new List<ModuleCalculationReport>();
        List<ParameterCalculationReport> parametersCalculations = new List<ParameterCalculationReport>();

        public CalculationReportForm()
        {
            InitializeComponent();
        }

        internal void SetData (List<ParameterValidationReport> validations, List<OperationReport> calculations)
        {
            if (validations != null)
                this.validations = validations;

            if (calculations != null)
            {
                modulesCalculations = calculations.Where(r => r is ModuleCalculationReport).Select(r => r as ModuleCalculationReport).ToList();
                parametersCalculations = calculations.Where(r => r is ParameterCalculationReport).Select(r => r as ParameterCalculationReport).ToList();

            }

            ReloadChanges();
            ReloadIssues();
        }

        void ReloadChanges ()
        {
            changesTable.Visible = false;
            changesTable.Controls.Clear();
            changesTable.RowStyles.Clear();

            var factory = new UIFactory();
            var succes = parametersCalculations.Where(r => r.IsSuccess);
            var succesChanged = succes.Where(r => r.WasChanged).ToList();
            var ordered = succesChanged.OrderBy(r => r.parameter.title);
            changesTable.RowCount = ordered.Count();

            foreach (ParameterCalculationReport report in ordered)
            {
                Panel row = factory.RowForReport(report);
                changesTable.Controls.Add(row);
            }

            changesTab.Text = string.Format("Изменения ({0})", succesChanged.Count());

            Invalidate(true);
            changesTable.Visible = true;
        }

        void ReloadIssues()
        {
            issuesTable.Visible = false;
            issuesTable.Controls.Clear();
            issuesTable.RowStyles.Clear();
            issuesTable.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            var totalIssues = 0;
            totalIssues += AddIssuesGroup("Проблемы расчетов модулей", modulesCalculations);
            totalIssues += AddIssuesGroup("Проблемы расчетов параметров", parametersCalculations);
            totalIssues += AddIssuesGroup("Проблемы валидации", validations); 

            issuesTab.Text = string.Format("Проблемы ({0})", totalIssues);

            Invalidate(true);
            issuesTable.Visible = true;
        }

        private int AddIssuesGroup(string headerTitle, IEnumerable<OperationReport> reports)
        {
            var factory = new UIFactory();

            var failed = reports.Where(r => !r.IsSuccess);
            var ordered = failed.OrderBy(r => r.operationTitle);

            issuesTable.RowCount += failed.Count();

            if (failed.Count() > 0)
            {
                issuesTable.RowCount += 1;
                Panel header = factory.HeaderForIssues(headerTitle);
                issuesTable.Controls.Add(header);
            }

            foreach (OperationReport report in ordered)
            {
                Panel row = factory.RowForReport(report);
                issuesTable.Controls.Add(row);
            }

            return failed.Count();
        }
    }
}
