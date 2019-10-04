using System;
using System.Windows.Forms;
using System.Drawing;
using System.Linq;

using ModelAnalyzer.Services;
using ModelAnalyzer.Parameters;

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

        private Panel ParameterComparasionRow(Parameter left, Parameter right)
        {
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

            Panel leftComparasionPanel = ComparasionLabelsPanel(DockStyle.Left);
            Panel rightComparasionPanel = ComparasionLabelsPanel(DockStyle.Right);

            var leftValue = valuesPanels.ValuePanel(left, true);
            leftValue.MaximumSize = leftComparasionPanel.Size;
            leftComparasionPanel.Controls.Add(leftValue);

            var rightValue = valuesPanels.ValuePanel(right, true);
            rightValue.MaximumSize = rightComparasionPanel.Size;
            rightComparasionPanel.Controls.Add(rightValue);

            comparasionPanel.Controls.Add(separatorLabel);
            comparasionPanel.Controls.Add(leftComparasionPanel);
            comparasionPanel.Controls.Add(rightComparasionPanel);

            panel.Controls.Add(comparasionPanel);
            panel.Controls.Add(components.TitleLabel(right.title));
            panel.Controls.Add(components.TypeIndicator(right.type));

            return panel;
        }

        internal Panel RowForReport(OperationReport report)
        {
            var issues = report.issues;

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

            panel.Controls.Add(issuesLabel);

            Label title = components.TitleLabel(report.operationTitle);
            title.Dock = DockStyle.Top;
            title.Font = new Font(title.Font, FontStyle.Bold);

            panel.Controls.Add(title);

            if (report is ParameterOperationReport parameterReport)
                CustomiseForParameterReport(panel, parameterReport);

            return panel;
        }

        private void CustomiseForParameterReport (Panel panel, ParameterOperationReport report)
        {
            panel.Controls.Add(components.TypeIndicator(report.parameter.type));
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
