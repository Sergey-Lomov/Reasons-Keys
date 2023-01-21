using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using ModelAnalyzer.Services;
using ModelAnalyzer.Parameters;

namespace ModelAnalyzer.UI.DetailsForms
{
    public partial class DigitalParameterDetailsForm : Form, IParameterDetailsForm
    {
        private readonly string issueItemPrefix = "- ";
        private readonly int unroundFractionalDigits = 3;

        DigitalParameter parameter;

        public DigitalParameterDetailsForm()
        {
            InitializeComponent();
        }

        public void SetParameter (Parameter _parameter, ParameterValidationReport validation)
        {
            if (!(_parameter is DigitalParameter))
                return;

            parameter = _parameter as DigitalParameter;
            bool isParameterIn = parameter.type == ParameterType.In;

            titleLabel.Text = parameter.title;
            detailsLabel.Text = parameter.details;

            if (parameter is FloatSingleParameter)
            {
                var single = parameter as FloatSingleParameter;
                valueLabel.Text = FloatStringConverter.FloatToString(single.GetValue(), single.fractionalDigits);
                unroundValueLabel.Text = FloatStringConverter.FloatToString(single.GetUnroundValue(), unroundFractionalDigits);
            } else if (parameter is FloatArrayParameter)
            {
                var array = parameter as FloatArrayParameter;
                valueLabel.Text = FloatStringConverter.ListToString(array.GetValue(), array.fractionalDigits);
                unroundValueLabel.Text = FloatStringConverter.ListToString(array.GetUnroundValue(), unroundFractionalDigits);
            }

            unroundValueLabel.Visible = !isParameterIn;

            var issues = new List<string>();
            if (parameter.calculationReport != null) 
                issues.AddRange(parameter.calculationReport.GetIssues());

            issues.AddRange(validation.GetIssues());
            issuesLabel.Text = issues.ToIssuesList(issueItemPrefix);

            detailsTitleLabel.Visible = detailsLabel.Text.Length > 0;
            valueTitleLabel.Visible = valueLabel.Text.Length > 0;
            unroundValueTitleLabel.Visible = unroundValueLabel.Text.Length > 0 && !isParameterIn;
            issuesTitleLabel.Visible = issuesLabel.Text.Length > 0;

            valueTitleLabel.Text = isParameterIn ? "Значение" : "Округленное";
        }
    }
}
