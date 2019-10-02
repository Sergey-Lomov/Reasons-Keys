using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

using ModelAnalyzer.Parameters;

namespace ModelAnalyzer.UI
{
    public partial class CalculationReportForm : Form
    {
        List<ParameterValidationReport> validations = new List<ParameterValidationReport>();
        List<ParameterCalculationReport> calculations = new List<ParameterCalculationReport>();

        public CalculationReportForm()
        {
            InitializeComponent();
        }

        internal void SetData (List<ParameterValidationReport> validations, List<ParameterCalculationReport> calculations)
        {
            if (validations != null)
                this.validations = validations;

            if (calculations != null)
                this.calculations = calculations;

            ReloadChanges();
            ReloadIssues();
        }

        void ReloadChanges ()
        {
            changesTable.Visible = false;
            changesTable.Controls.Clear();
            changesTable.RowStyles.Clear();

            var factory = new UIFactory();
            var succes = calculations.Where(r => r.IsSucces);
            var succesChanged = succes.Where(r => r.WasChanged).ToList();
            var ordered = succesChanged.OrderBy(r => r.parameter.title);
            changesTable.RowCount = ordered.Count();

            foreach (ParameterCalculationReport report in ordered)
            {
                Panel row = factory.RowForCalculationReport(report);
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

            var factory = new UIFactory();

            // Calculation issues
            var failedCalculations = calculations.Where(r => !r.IsSucces);
            var orderedCalculations = failedCalculations.OrderBy(r => r.parameter.title);

            issuesTable.RowCount += failedCalculations.Count();
            if (failedCalculations.Count() > 0)
            {
                issuesTable.RowCount += 1;
                Panel header = factory.HeaderForIssues("Проблемы расчетов");
                issuesTable.Controls.Add(header);
            }

            foreach (ParameterCalculationReport report in orderedCalculations)
            {
                Panel row = factory.RowForCalculationReport(report);
                issuesTable.Controls.Add(row);
            }

            // Validation issues
            var failedValidations = validations.Where(r => r.HasIssues);
            var orderedValidations = failedValidations.OrderBy(r => r.parameter.title);
            issuesTable.RowCount += failedValidations.Count();

            if (failedValidations.Count() > 0)
            {
                issuesTable.RowCount += 1;
                Panel header = factory.HeaderForIssues("Проблемы валидации");
                issuesTable.Controls.Add(header);
            }

            foreach (ParameterValidationReport report in orderedValidations)
            {
                Panel row = factory.RowForValidationReport(report);
                issuesTable.Controls.Add(row);
            }

            issuesTab.Text = string.Format("Проблемы ({0})", failedValidations.Count() + failedCalculations.Count());

            Invalidate(true);
            issuesTable.Visible = true;
        }
    }
}
