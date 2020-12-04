using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using ModelAnalyzer.Parameters;

namespace ModelAnalyzer.UI.DetailsForms
{
    public partial class FieldNodesDetailsForm : Form, IParameterDetailsForm
    {
        private const int mapInsets = 30;
        private int fieldDiameter;
        FieldNodesParameter parameter;

        internal Color averageColor = Color.FromArgb(102, 159, 88);
        internal Color maxDeviationColor = Color.FromArgb(232, 230, 73);

        public FieldNodesDetailsForm()
        {
            InitializeComponent();
            mapPanel.Paint += new PaintEventHandler(drawMap);
        }

        public void SetParameter(Parameter _parameter, ParameterValidationReport validation)
        {
            if (!(_parameter is FieldNodesParameter))
                return;

            parameter = _parameter as FieldNodesParameter;
            bool isParameterIn = parameter.type == ParameterType.In;

            titleLabel.Text = parameter.title;
            detailsLabel.Text = parameter.details;

            var fieldRadius = parameter.field.Keys.Select(p => p.radius()).Max();
            fieldDiameter = fieldRadius * 2 + 1;
        }

        private void drawMap(object sender, PaintEventArgs e)
        {
            var drawwer = new FieldDrawwer(new Pen(Color.Black, 2),
                new Font("Helvetica", 8, FontStyle.Regular));

            var fieldCenter = new Point(mapPanel.Bounds.Width / 2, mapPanel.Bounds.Height / 2);
            var graphics = mapPanel.CreateGraphics();
            graphics.Clear(Color.White);

            var maxWidthRadius = (mapPanel.Bounds.Width - 2 * mapInsets) / (1.5 * (float)fieldDiameter + 0.5);
            var maxHeightRadius = (mapPanel.Bounds.Height - 2 * mapInsets) / (Math.Sqrt(3) * fieldDiameter);
            var radius = (int)Math.Min(maxHeightRadius, maxWidthRadius);

            var averageValue = parameter.field.Values.Distinct().Average();
            var maxDeviation = parameter.field.Values.Select(v => Math.Abs(v - averageValue)).Max();
            Func<float, float, float, int> gradientPosition = (c1, c2, p) => (int)(c1 + (c2 - c1) * p);

            foreach (var pair in parameter.field)
            {
                var title = pair.Key.x.ToString() + ", " + pair.Key.y.ToString() + ", " + pair.Key.z.ToString();
                var relativeDeviation = maxDeviation != 0 ? Math.Abs(pair.Value - averageValue) / maxDeviation : 0;
                var r = gradientPosition(averageColor.R, maxDeviationColor.R, relativeDeviation);
                var g = gradientPosition(averageColor.G, maxDeviationColor.G, relativeDeviation);
                var b = gradientPosition(averageColor.B, maxDeviationColor.B, relativeDeviation);
                var color = Color.FromArgb(r, g, b);
                drawwer.drawPoint(pair.Key, radius, fieldCenter, graphics, color, title);
            }
        }
    }
}
