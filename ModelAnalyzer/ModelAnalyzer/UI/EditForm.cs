using System;
using System.Windows.Forms;

namespace ModelAnalyzer.UI
{
    public partial class EditForm : Form
    {
        public bool calculation = false;

        public EditForm()
        {
            InitializeComponent();
        }

        public void SetValue(String value)
        {
            textBox.Text = value;
        }

        public string GetValue()
        {
            return textBox.Text;
        }

        private void CalculateClick(object sender, EventArgs e)
        {
            calculation = true;
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                Close();
                DialogResult = DialogResult.OK;
            }

            if (e.KeyCode == Keys.Escape)
            {
                e.Handled = true;
                Close();
                DialogResult = DialogResult.Cancel;
            }
        }
    }
}
