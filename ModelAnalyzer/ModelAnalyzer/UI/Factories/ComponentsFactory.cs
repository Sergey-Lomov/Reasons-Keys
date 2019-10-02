using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

using ModelAnalyzer.Parameters;

namespace ModelAnalyzer.UI.Factories
{
    class ComponentsFactory
    {
        private UIConstants constants = new UIConstants();

        Dictionary<ParameterType, Color> typesColors = new Dictionary<ParameterType, Color>();

        private readonly Font headerFont = new Font("Serif", 10, FontStyle.Bold);
        private readonly Color issueColor = Color.FromArgb(255, 50, 50);
        private readonly int typeIndicatorWidth = 10;

        public ComponentsFactory ()
        {
            Color inColor = Color.FromArgb(128, 200, 128);
            Color outColor = Color.FromArgb(128, 128, 200);
            Color innerColor = Color.FromArgb(200, 200, 200);
            Color indicatorColor = Color.FromArgb(220, 220, 110);

            typesColors.Add(ParameterType.In, inColor);
            typesColors.Add(ParameterType.Out, outColor);
            typesColors.Add(ParameterType.Inner, innerColor);
            typesColors.Add(ParameterType.Indicator, indicatorColor);
        }

        internal Panel RowPanel(Color backColor, int height)
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

        internal Panel TypeIndicator(ParameterType type)
        {
            return new Panel()
            {
                BackColor = typesColors[type],
                Width = typeIndicatorWidth,
                Dock = DockStyle.Left
            };
        }

        internal Panel IssuesIndicator(Parameter parameter, ParameterValidationReport validation)
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

        internal Label TitleLabel(string title)
        {
            return new Label()
            {
                Text = title,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = UIConstants.labelsPadding
            };
        }

        internal Label HeaderTitleLabel(string title)
        {
            return new Label()
            {
                Text = title,
                Font = headerFont,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = UIConstants.labelsPadding
            };
        }
    }
}
