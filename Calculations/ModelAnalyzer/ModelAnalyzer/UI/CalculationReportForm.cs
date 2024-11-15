﻿using System.Collections.Generic;
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
        ModelCalcultaionReport report;

        public CalculationReportForm()
        {
            InitializeComponent();
        }

        internal void SetData (List<ParameterValidationReport> validations, ModelCalcultaionReport report)
        {
            this.report = report;
            if (validations != null)
                this.validations = validations;

            modulesCalculations = report.operations.Where(r => r is ModuleCalculationReport).Select(r => r as ModuleCalculationReport).ToList();
            parametersCalculations = report.operations.Where(r => r is ParameterCalculationReport).Select(r => r as ParameterCalculationReport).ToList();

            ReloadChanges();
            ReloadIssues();
            ReloadUnused();
            ReloadTiming();
        }

        void ReloadChanges ()
        {
            changesTable.Visible = false;
            changesTable.Controls.Clear();
            changesTable.RowStyles.Clear();

            var factory = new UIFactory();
            var succes = parametersCalculations.Where(r => r.IsSuccess);
            var succesChanged = succes.Where(r => r.WasChanged).ToList();
            var grouped = succesChanged
                .Select(r => new { r.parameter.grade, r })
                .GroupBy(p => p.grade)
                .Select(g => g.Select(p => p.r).ToList())
                .OrderBy(g => g[0].parameter.grade)
                .ToList();
            
            changesTable.RowCount = succesChanged.Count() + grouped.Count;

            foreach (var group in grouped)
            {
                if (group.Count == 0) continue;

                var grade = group[0].parameter.grade;
                Panel header = factory.HeaderForGadeGroup(grade);
                changesTable.Controls.Add(header);

                var ordered = group.OrderBy(r => r.parameter.title);
                foreach (ParameterCalculationReport report in ordered)
                {
                    Panel row = factory.ParameterComparasionRow(report.precalculated, report.parameter);
                    changesTable.Controls.Add(row);
                }
            }

            changesTab.Text = string.Format("Изменения ({0})", succesChanged.Count());

            changesTable.Invalidate(true);
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

            issuesTable.Invalidate(true);
            issuesTable.Visible = true;
        }

        void ReloadUnused()
        {
            unusedTable.Visible = false;
            unusedTable.Controls.Clear();
            unusedTable.RowStyles.Clear();
            unusedTable.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            var unused = parametersCalculations
                .Select(r => r.parameter)
                .Where(p => p.IsUnused())
                .ToList();

            unusedTab.Text = string.Format("Неиспользуемый ({0})", unused.Count);

            var factory = new UIFactory();
            foreach (var parameter in unused)
            {
                Panel row = factory.RowForUnusedParameter(parameter);
                unusedTable.Controls.Add(row);
            }

            unusedTable.Invalidate(true);
            unusedTable.Visible = true;
        }

        void ReloadTiming()
        {
            timingTable.Visible = false;
            timingTable.Controls.Clear();
            timingTable.RowStyles.Clear();
            timingTable.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            var factory = new UIFactory();
            Panel totalRow = factory.RowForTiming("Общее время", report.duration);
            timingTable.Controls.Add(totalRow);

            var operations = report.operations.OrderByDescending(o => o.duration);
            foreach (var operationReport in operations)
            {
                Panel row = factory.RowForTiming(operationReport);
                timingTable.Controls.Add(row);
            }

            timingTable.Invalidate(true);
            timingTable.Visible = true;
        }

        private int AddIssuesGroup(string headerTitle, IEnumerable<OperationReport> reports)
        {
            var factory = new UIFactory();

            var failed = reports.Where(r => !r.IsSuccess);
            var ordered = failed.OrderBy(r => r.OperationTitle);

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
