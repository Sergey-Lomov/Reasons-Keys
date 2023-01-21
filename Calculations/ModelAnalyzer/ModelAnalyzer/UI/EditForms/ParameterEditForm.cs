using System;
using System.Windows.Forms;

using ModelAnalyzer.Parameters;

namespace ModelAnalyzer.UI
{
    public class ParameterEditForm : Form
    {
        protected bool modelUpdateNecessary = false;
        protected bool parameterUpdateNecessary = false;

        virtual internal bool IsParameterUpdateNecessary() { return parameterUpdateNecessary; }
        virtual internal bool IsModelUpdateNecessary() { return modelUpdateNecessary; }
        virtual internal void SetParameter(Parameter p) { }
        virtual internal void UpdateParameter() { }
        virtual internal Parameter GetParameter() { return null;}

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                Approve();
            }

            if (e.KeyCode == Keys.Escape)
            {
                e.Handled = true;
                Cancel();
            }
        }

        protected void Approve()
        {
            UpdateParameter();
            Close();
        }

        protected void Calculate()
        {
            UpdateParameter();
            modelUpdateNecessary = true;
            Close();
        }
        protected void Cancel()
        {
            parameterUpdateNecessary = false;
            modelUpdateNecessary = false;
            Close();
        }
    }
}
