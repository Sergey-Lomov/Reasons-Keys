using System.Windows.Forms;
using System.Drawing;

using ModelAnalyzer.Parameters;

namespace ModelAnalyzer.UI.Factories
{
    class ValuePanelsFactory
    {
        public Panel ValuePanel(Parameter p, Color backColor, int height)
        {
            var panel = new Panel();

            if (p is FloatSingleParameter)
                AddFloatSingle(p as FloatSingleParameter, panel);
            if (p is FloatArrayParameter)
                AddFloatArray(p as FloatArrayParameter, panel);

            return panel;
        }

        private void AddFloatSingle(FloatSingleParameter p, Panel panel)
        {
            Label value = new Label()
            {
                Text = p.ValueToString(),
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = UIConstants.labelsPadding,
            };

            panel.Controls.Add(value);
        }

        private void AddFloatArray(FloatArrayParameter p, Panel panel)
        {
            Label value = new Label()
            {
                Text = p.ValueToString(),
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = UIConstants.labelsPadding,
              //  Width = UIConstants.valuePanelWidth
            };
            panel.Controls.Add(value);
        }
    }
}
