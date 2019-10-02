using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.Linq;

using ModelAnalyzer.Parameters;
using System;

namespace ModelAnalyzer.UI.Factories
{
    class CalculationReportRowsFactory
    {
        private ComponentsFactory components = new ComponentsFactory();
        private ValuePanelsFactory valuesPanels = new ValuePanelsFactory();

        private readonly Color reportRowBack = Color.FromArgb(240, 240, 240);

        private readonly int reportRowHeight = 50;
        private readonly int comparasionLabelsWidth = 150;
        private readonly int comparasionSeparatorWidth = 20;
        private readonly int issuesHPadding = 30;

        private string comparasionSeparator = "=>";
        private string issueItemPrefix = "- ";

        public Panel HeaderForIssues(string title)
        {
            Panel panel = components.RowPanel(reportRowBack, UIConstants.headerRowHeight);
            panel.Controls.Add(components.HeaderTitleLabel(title));
            return panel;
        }

        public Panel RowForValidationReport(ParameterValidationReport report)
        {
            return RowForIssues(report.parameter, report.issues);
        }

        public Panel RowForCalculationReport(ParameterCalculationReport report)
        {
            return report.IsSucces ? RowForSuccesCalculation(report) : RowForFailedCalculation(report);
        }

        private Panel RowForSuccesCalculation(ParameterCalculationReport report)
        {
            var parameter = report.parameter;
            Panel panel = components.RowPanel(reportRowBack, reportRowHeight);

            int valuesPanelWidth = comparasionLabelsWidth * 2 + comparasionSeparatorWidth;
            Panel comparasionPanel = new Panel()
            {
                Dock = DockStyle.Right,
                AutoSize = true,
                MaximumSize = new Size(valuesPanelWidth, reportRowHeight),
                MinimumSize = new Size(valuesPanelWidth, reportRowHeight)
            };

            Label separatorLabel = new Label()
            {
                Text = comparasionSeparator,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
            };

            Panel oldPanel = ComparasionLabelsPanel(DockStyle.Left);
            Panel newPanel = ComparasionLabelsPanel(DockStyle.Right);

            var oldValue = valuesPanels.ValuePanel(report.precalculated, true);
            oldValue.MaximumSize = oldPanel.Size;
            oldPanel.Controls.Add(oldValue);

            var newValue = valuesPanels.ValuePanel(report.parameter, true);
            newValue.MaximumSize = newPanel.Size;
            newPanel.Controls.Add(newValue);

            comparasionPanel.Controls.Add(separatorLabel);
            comparasionPanel.Controls.Add(oldPanel);
            comparasionPanel.Controls.Add(newPanel);

            panel.Controls.Add(comparasionPanel);
            panel.Controls.Add(components.TitleLabel(parameter.title));
            panel.Controls.Add(components.TypeIndicator(parameter.type));

            return panel;
        }

        private Panel RowForFailedCalculation(ParameterCalculationReport report)
        {
            return RowForIssues(report.parameter, report.issues);
        }

        private Panel RowForIssues(Parameter parameter, List<string> issues)
        {
            Panel panel = new Panel()
            {
                BackColor = reportRowBack,
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink
            };

            string issuesString = "";
            foreach (string issue in issues)
            {
                var prefix = issues.Count > 1 ? issueItemPrefix : "";
                issuesString += prefix + issue;
                if (issue != issues.Last())
                    issuesString += Environment.NewLine;
            }

            Label issuesLabel = new Label()
            {
                Text = issuesString,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(issuesHPadding, 0, UIConstants.labelsHPadding, UIConstants.labelsVPadding * 2),
                AutoSize = true
            };

            Label title = components.TitleLabel(parameter.title);
            title.Dock = DockStyle.Top;
            title.Font = new Font(title.Font, FontStyle.Bold);

            panel.Controls.Add(issuesLabel);
            panel.Controls.Add(title);
            panel.Controls.Add(components.TypeIndicator(parameter.type));

            return panel;
        }

        private Label ComparasionLabel(string value, bool isNew, bool isRounded)
        {
            return new Label()
            {
                Text = value,
                Dock = isRounded ? DockStyle.Top : DockStyle.Bottom,
                TextAlign = isNew ? ContentAlignment.MiddleLeft : ContentAlignment.MiddleRight,
                Padding = UIConstants.labelsPadding,
                Width = comparasionLabelsWidth,
                Height = reportRowHeight / 2
            };
        }

        private Panel ComparasionLabelsPanel(DockStyle dock)
        {
            return new Panel()
            {
                Dock = dock,
                AutoSize = true,
                MaximumSize = new Size(comparasionLabelsWidth, reportRowHeight),
                MinimumSize = new Size(comparasionLabelsWidth, reportRowHeight)
            };
        }
    }
}
