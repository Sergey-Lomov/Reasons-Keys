using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using ModelAnalyzer.Parameters;

namespace ModelAnalyzer.UI
{
    public partial class PairsArrayDetailsForm : Form, IParameterDetailsForm
    {
        private readonly string issueItemPrefix = "- ";

        public PairsArrayDetailsForm()
        {
            InitializeComponent();
        }

        public void SetParameter(Parameter _parameter, ParameterValidationReport validation)
        {
            if(!(_parameter is PairsArrayParameter))
                return;

            var parameter = _parameter as PairsArrayParameter;

            titleLabel.Text = parameter.title;
            detailsLabel.Text = parameter.details;

            var value = parameter.GetValue();
            var firstSequence = value.Select(pair => pair.Item1);
            var secondSequence = value.Select(pair => pair.Item2);

            valueTable.Controls.Clear();
            valueTable.ColumnStyles.Clear();

            for (int i = 0; i < value.Count(); i++)
                valueTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100 / value.Count()));

            valueTable.ColumnCount = value.Count();

            valueTable.RowCount = 2;

            foreach (var element in firstSequence)
                valueTable.Controls.Add(ElementLabel(element));
            foreach (var element in secondSequence)
                valueTable.Controls.Add(ElementLabel(element));

            var issues = new List<string>();
            if (parameter.calculationReport != null)
                issues.AddRange(parameter.calculationReport.issues);

            issues.AddRange(validation.issues);

            issuesLabel.Text = "";
            foreach (string issue in issues)
            {
                var prefix = issues.Count > 1 ? issueItemPrefix : "";
                issuesLabel.Text += prefix + issue;
                if (issue != issues.Last())
                    issuesLabel.Text += Environment.NewLine;
            }

            detailsTitleLabel.Visible = detailsLabel.Text.Length > 0;
            issuesTitleLabel.Visible = issuesLabel.Text.Length > 0;
        }

        private Label ElementLabel(int element)
        {
            return new Label()
            {
                Text = element.ToString(),
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Padding = UIConstants.labelsPadding,
            };
        }
    }
}
