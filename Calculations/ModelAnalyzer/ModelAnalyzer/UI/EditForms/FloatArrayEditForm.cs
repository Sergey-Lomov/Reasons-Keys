using System;
using System.Linq;
using System.Windows.Forms;

using ModelAnalyzer.Services;
using ModelAnalyzer.Parameters;

namespace ModelAnalyzer.UI
{
    public partial class FloatArrayEditForm : ParameterEditForm
    {
        private FloatArrayParameter parameter;

        public FloatArrayEditForm()
        {
            InitializeComponent();
        }

        internal override void SetParameter(Parameter p)
        {
            if (!(p is FloatArrayParameter))
                return;

            parameter = p as FloatArrayParameter;
            textBox.Text = FloatStringConverter.ListToString(parameter.GetValue(), parameter.fractionalDigits);
        }

        internal override Parameter GetParameter()
        {
            return parameter;
        }

        internal override void UpdateParameter ()
        {
            parameter.SetValue(FloatStringConverter.ListFromString(textBox.Text));
            parameterUpdateNecessary = true;
        }

        private void ApproveButton_Click(object sender, EventArgs e) { Approve(); }
        private void CalculateButton_Click(object sender, EventArgs e) { Calculate(); }
        private void CancelButton_Click(object sender, EventArgs e) { Cancel(); }
    }
}
