using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;
using System.Drawing;

namespace ModelAnalyzer
{
    class UIFactory
    {
        readonly Color parameterRowBack = Color.FromArgb(250, 250, 250);
        readonly Color reportRowBack = Color.FromArgb(240, 240, 240);
        readonly Color issueColor = Color.FromArgb(255, 50, 50);
        readonly Font headerFont = new Font("Serif", 10, FontStyle.Bold);

        Dictionary<ParameterType, Color> typesColors = new Dictionary<ParameterType, Color>();
        Dictionary<ParameterType, string> typesTitles = new Dictionary<ParameterType, string>();

        const int parameterRowHeight = 35;
        const int reportRowHeight = 65;
        const int headerRowHeight = 25;
        const int valueLabelWidth = 125;
        const int comparasionLabelsWidth = 125;
        const int comparasionSeparatorWidth = 20;
        const int typeIndicatorWidth = 10;
        const int hPadding = 10;
        const int vPadding = 3;
        const int issuesHPadding = 30;

        const string comparasionSeparator = "=>";
        const string issueItemPrefix = "- ";

        public UIFactory()
        {
            Color inColor = Color.FromArgb(128, 200, 128);
            Color outColor = Color.FromArgb(128, 128, 200);
            Color innerColor = Color.FromArgb(200, 200, 200);
            Color indicatorColor = Color.FromArgb(220, 220, 110);

            typesColors.Add(ParameterType.In, inColor);
            typesColors.Add(ParameterType.Out, outColor);
            typesColors.Add(ParameterType.Inner, innerColor);
            typesColors.Add(ParameterType.Indicator, indicatorColor);

            typesTitles.Add(ParameterType.In, "Входящие");
            typesTitles.Add(ParameterType.Out, "Исходящие");
            typesTitles.Add(ParameterType.Inner, "Внутренние");
            typesTitles.Add(ParameterType.Indicator, "Индикаторы");
        }

        private Panel RowPanel(Color backColor, int height)
        {
            return new Panel()
            {
                BackColor = backColor,
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                AutoSize = true,
                MaximumSize = new Size(0, height),
                MinimumSize = new Size(0, height)
            };
        }

        private Panel TypeIndicator (ParameterType type)
        {
            return new Panel()
            {
                BackColor = typesColors[type],
                Width = typeIndicatorWidth,
                Dock = DockStyle.Left
            };
        }

        private Panel IssuesIndicator(Parameter parameter, ParameterValidationReport validation)
        {
            var hasIssues = validation.HasIssues || parameter.calculationReport?.IsSucces == false;
            Color issuesIndicatorColor = hasIssues ? issueColor : Color.Transparent;
            return new Panel()
            {
                BackColor = issuesIndicatorColor,
                Width = 10,
                Dock = DockStyle.Right
            };
        }

        private Label HeaderTitleLabel(string title)
        {
            return new Label()
            {
                Text = title,
                Font = headerFont,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(hPadding, vPadding, hPadding, vPadding)
            };
        }

        private Label TitleLabel(string title)
        {
            return new Label()
            {
                Text = title,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(hPadding, vPadding, hPadding, vPadding)
            };
        }

        private Label ComparasionLabel(string value, bool isNew, bool isRounded)
        {
            return new Label()
            {
                Text = value,
                Dock = isRounded ? DockStyle.Top : DockStyle.Bottom,
                TextAlign = isNew ? ContentAlignment.MiddleLeft : ContentAlignment.MiddleRight,
                Padding = new Padding(hPadding, vPadding, hPadding, vPadding),
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

        public Panel HeaderForIssues(string title)
        {
            Panel panel = RowPanel(reportRowBack, headerRowHeight);
            panel.Controls.Add(HeaderTitleLabel(title));
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
            Panel panel = RowPanel(reportRowBack, reportRowHeight);

            int valuesPanelWidth = comparasionLabelsWidth * 2 + comparasionSeparatorWidth;
            Panel comparasionPanel = new Panel()
            {
                Dock = DockStyle.Right,
                AutoSize = true,
                MaximumSize = new Size(valuesPanelWidth, reportRowHeight),
                MinimumSize = new Size(valuesPanelWidth, reportRowHeight)
            };

            Label comparasionSeparator = new Label()
            {
                Text = UIFactory.comparasionSeparator,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
            };

            Panel oldPanel = ComparasionLabelsPanel(DockStyle.Left);
            Panel newPanel = ComparasionLabelsPanel(DockStyle.Right);

            Label oldValue = ComparasionLabel(report.oldValueString, false, true);
            Label oldUnroundValue = ComparasionLabel(report.oldUnroundValueString, false, false);
            Label newValue = ComparasionLabel(parameter.ValueToString(), true, true);
            Label newUnroundValue = ComparasionLabel(parameter.UnroundValueToString(), true, false);

            oldPanel.Controls.Add(oldValue);
            oldPanel.Controls.Add(oldUnroundValue);
            newPanel.Controls.Add(newValue);
            newPanel.Controls.Add(newUnroundValue);

            comparasionPanel.Controls.Add(comparasionSeparator);
            comparasionPanel.Controls.Add(oldPanel);
            comparasionPanel.Controls.Add(newPanel);

            panel.Controls.Add(comparasionPanel);
            panel.Controls.Add(TitleLabel(parameter.title));
            panel.Controls.Add(TypeIndicator(parameter.type));

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
                Padding = new Padding(issuesHPadding, 0, hPadding, vPadding * 2),
                AutoSize = true
            };

            Label title = TitleLabel(parameter.title);
            title.Dock = DockStyle.Top;
            title.Font = new Font(title.Font, FontStyle.Bold);

            panel.Controls.Add(issuesLabel);
            panel.Controls.Add(title);
            panel.Controls.Add(TypeIndicator(parameter.type));

            return panel;
        }

        public Panel HeaderForParameterType(ParameterType type)
        {
            var panel = RowPanel(parameterRowBack, headerRowHeight);
            var titleLabel = HeaderTitleLabel(typesTitles[type]);
            panel.Controls.Add(titleLabel);

            return panel;
        }

        public Panel RowForParameter(Parameter parameter, IParameterRowDelegate rowDelegate, ParameterValidationReport validation)
        {
            Panel panel = RowPanel(parameterRowBack, parameterRowHeight);

            Label title = TitleLabel(parameter.title);
            title.Click += (sender, e) => rowDelegate.HandleTitleClick(parameter, title);

            Label value = new Label()
            {
                Text = parameter.ValueToString(),
                Dock = DockStyle.Right,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(hPadding, vPadding, hPadding, vPadding),
                Width = valueLabelWidth
            };

            if (parameter.type == ParameterType.In)
                value.Click += (sender, e) => rowDelegate.HandleValueClick(parameter, value);

            panel.Controls.Add(title);
            panel.Controls.Add(TypeIndicator(parameter.type));
            panel.Controls.Add(value);
            panel.Controls.Add(IssuesIndicator(parameter, validation));

            return panel;
        }
    }
}
