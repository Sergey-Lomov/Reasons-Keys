using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ModelAnalyzer.Parameters;

namespace ModelAnalyzer.UI.EditForms
{
    public partial class BoolEditForm : ParameterEditForm
    {
        private BoolParameter parameter;

        public BoolEditForm()
        {
            InitializeComponent();
        }

        internal override void SetParameter(Parameter p)
        {
            if (!(p is BoolParameter))
                return;

            parameter = p as BoolParameter;
            valueCheckBox.Checked = parameter.GetValue();
            valueCheckBox.Text = parameter.EditorLabel();
        }

        internal override Parameter GetParameter()
        {
            return parameter;
        }

        internal override void UpdateParameter()
        {
            parameter.SetValue(valueCheckBox.Checked);
            parameterUpdateNecessary = true;
        }

        private void ApproveButton_Click(object sender, EventArgs e) { Approve(); }
        private void CalculateButton_Click(object sender, EventArgs e) { Calculate(); }
        private void CancelButton_Click(object sender, EventArgs e) { Cancel(); }
    }
}
