using ModelAnalyzer.Parameters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ModelAnalyzer.Services;

namespace ModelAnalyzer.UI.DetailsForms
{
    public partial class BoolDetailsForm : Form, IParameterDetailsForm
    {
        private readonly string issueItemPrefix = "- ";

        BoolParameter parameter;
        public BoolDetailsForm()
        {
            InitializeComponent();
        }

        public void SetParameter(Parameter _parameter, ParameterValidationReport validation)
        {
            if (!(_parameter is BoolParameter))
                return;

            parameter = _parameter as BoolParameter;
            titleLabel.Text = parameter.title;
            detailsLabel.Text = parameter.details;
            valueLabel.Text = parameter.GetNullableValue().HumanRedeable();

            var issues = new List<string>();
            if (parameter.calculationReport != null)
                issues.AddRange(parameter.calculationReport.GetIssues());

            issues.AddRange(validation.GetIssues());
            issuesLabel.Text = issues.ToIssuesList(issueItemPrefix);
            issuesTitleLabel.Visible = issuesLabel.Text.Length > 0;
        }
    }
}
