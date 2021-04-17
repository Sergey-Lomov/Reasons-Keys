using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using ModelAnalyzer.Parameters;
using ModelAnalyzer.Services;

namespace ModelAnalyzer.UI.DetailsForms
{
    public partial class FieldNodesDetailsForm : Form, IParameterDetailsForm
    {
        private const int mapInsets = 30;
        private Pen pen = new Pen(Color.Black, 2);
        private Font font = new Font("Helvetica", 12, FontStyle.Bold);

        private int fieldDiameter;
        FieldNodesFloatParameter parameter;

        internal Color averageColor = Color.FromArgb(102, 159, 88);
        internal Color middleDeviationColor = Color.FromArgb(232, 230, 73);
        internal Color maxDeviationColor = Color.FromArgb(161, 69, 52);

        public FieldNodesDetailsForm()
        {
            InitializeComponent();
            mapPanel.Paint += new PaintEventHandler(drawMap);
        }

        public void SetParameter(Parameter _parameter, ParameterValidationReport validation)
        {
            if (!(_parameter is FieldNodesFloatParameter))
                return;

            parameter = _parameter as FieldNodesFloatParameter;
            bool isParameterIn = parameter.type == ParameterType.In;

            titleLabel.Text = parameter.title;
            detailsLabel.Text = parameter.details;

            var fieldRadius = parameter.field.Keys.Select(p => p.radius()).Max();
            fieldDiameter = fieldRadius * 2 + 1;
        }

        private void drawMap(object sender, PaintEventArgs e)
        {
            var drawwer = new FieldDrawwer(pen, font);

            var fieldCenter = new Point(mapPanel.Bounds.Width / 2, mapPanel.Bounds.Height / 2);
            var graphics = mapPanel.CreateGraphics();
            graphics.Clear(Color.White);

            var maxWidthRadius = (mapPanel.Bounds.Width - 2 * mapInsets) / (1.5 * (float)fieldDiameter + 0.5);
            var maxHeightRadius = (mapPanel.Bounds.Height - 2 * mapInsets) / (Math.Sqrt(3) * fieldDiameter);
            var radius = (int)Math.Min(maxHeightRadius, maxWidthRadius);

            var averageValue = parameter.field.Values.Distinct().Average();
            var maxDeviation = parameter.field.Values.Select(v => Math.Abs(v - averageValue)).Max();

            foreach (var pair in parameter.field)
            {
                var title = FloatStringConverter.FloatToString(pair.Value, parameter.fractionalDigits);
                var deviation = parameter.deviationForValue(pair.Value);
                var color = deviation >= 0 && deviation <= 1 ? ColorForDeviation(deviation) : Color.White;
                drawwer.drawPoint(pair.Key, radius, fieldCenter, graphics, color, title);
            }
        }

        private Color ColorForDeviation(float deviation)
        {
            var startColor = deviation < 0.5 ? averageColor : middleDeviationColor;
            var endColor = deviation < 0.5 ? middleDeviationColor : maxDeviationColor;
            var normalisedDeviation = deviation < 0.5 ? deviation * 2 : (deviation - 0.5f) * 2;

            int gradientPosition(float c1, float c2, float p) => (int)(c1 + (c2 - c1) * p);
            var r = gradientPosition(startColor.R, endColor.R, normalisedDeviation);
            var g = gradientPosition(startColor.G, endColor.G, normalisedDeviation);
            var b = gradientPosition(startColor.B, endColor.B, normalisedDeviation);

            return Color.FromArgb(r, g, b);
        }
    }
}
