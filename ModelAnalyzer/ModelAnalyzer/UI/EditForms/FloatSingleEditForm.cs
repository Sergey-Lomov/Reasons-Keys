using System;
using System.Windows.Forms;

using ModelAnalyzer.Services;
using ModelAnalyzer.Parameters;

namespace ModelAnalyzer.UI
{
    public partial class FloatSingleEditForm : ModelAnalyzer.UI.ParameterEditForm
    {
        private FloatSingleParameter parameter;
        private bool modelUpdateNecessary = false;
        private bool parameterUpdateNecessary = false;

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

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                UpdateParameter();
                e.Handled = true;
                Close();
            }

            if (e.KeyCode == Keys.Escape)
            {
                parameterUpdateNecessary = false;
                modelUpdateNecessary = false;
                e.Handled = true;
                Close();
            }
        }

        internal override bool IsParameterUpdateNecessary()
        {
            return parameterUpdateNecessary;
        }

        internal override bool IsModelUpdateNecessary()
        {
            return modelUpdateNecessary;
        }

        private void UpdateParameter()
        {
            parameter.SetValue(FloatStringConverter.FloatFromString(textBox.Text));
            parameterUpdateNecessary = true;
        }

        private void approveButton_Click(object sender, EventArgs e)
        {
            UpdateParameter();
            Close();
        }

        private void calculateButton_Click(object sender, EventArgs e)
        {
            UpdateParameter();
            modelUpdateNecessary = true;
            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            parameterUpdateNecessary = false;
            modelUpdateNecessary = false;
            Close();
        }
    }
}
