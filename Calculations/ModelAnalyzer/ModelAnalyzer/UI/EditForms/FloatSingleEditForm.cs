﻿using System;
using System.Windows.Forms;

using ModelAnalyzer.Services;
using ModelAnalyzer.Parameters;

namespace ModelAnalyzer.UI
{
    public partial class FloatSingleEditForm : ParameterEditForm
    {
        private FloatSingleParameter parameter;
        public FloatSingleEditForm()
        {
            InitializeComponent();
        }

        internal override void SetParameter(Parameter p)
        {
            if (!(p is FloatSingleParameter))
                return;

            parameter = p as FloatSingleParameter;
            textBox.Text = FloatStringConverter.FloatToString(parameter.GetValue(), parameter.fractionalDigits);
        }

        internal override Parameter GetParameter()
        {
            return parameter;
        }

        internal override void UpdateParameter()
        {
            parameter.SetValue(FloatStringConverter.FloatFromString(textBox.Text));
            parameterUpdateNecessary = true;
        }

        private void ApproveButton_Click(object sender, EventArgs e) { Approve(); }
        private void CalculateButton_Click(object sender, EventArgs e) { Calculate(); }
        private void CancelButton_Click(object sender, EventArgs e) { Cancel(); }
    }
}
