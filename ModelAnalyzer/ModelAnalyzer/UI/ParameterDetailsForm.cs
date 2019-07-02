using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ModelAnalyzer.UI
{
    public partial class ParameterDetailsForm : Form, IParameterDetailsForm
    {
        private readonly string issueItemPrefix = "- ";

        Parameter parameter;

        public ParameterDetailsForm()
        {
            InitializeComponent();
        }

        public void SetParameter (Parameter parameter, ParameterValidationReport validation)
        {
            this.parameter = parameter;
            bool isParameterIn = parameter.type == ParameterType.In;

            titleLabel.Text = parameter.title;
            detailsLabel.Text = parameter.details;
            valueLabel.Text = parameter.ValueToString();
            unroundValueLabel.Text = parameter.UnroundValueToString();
            unroundValueLabel.Visible = !isParameterIn;

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
            valueTitleLabel.Visible = valueLabel.Text.Length > 0;
            unroundValueTitleLabel.Visible = unroundValueLabel.Text.Length > 0 && !isParameterIn;
            issuesTitleLabel.Visible = issuesLabel.Text.Length > 0;

            valueTitleLabel.Text = isParameterIn ? "Значение" : "Округленное";
        }
    }
}
