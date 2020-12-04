using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ModelAnalyzer.Services.FieldAnalyzer;

namespace ModelAnalyzer.UI
{
    class FieldDrawwer
    {
        public Pen pen;
        public Font font;

        public FieldDrawwer(Pen pen, Font font) 
        {
            this.pen = pen;
            this.font = font;
        }

        public void drawPoint(FieldPoint point, 
            float radius, 
            Point fieldCenter, 
            Graphics graphics,
            Color color,
            string title = "")
        {
            var col = point.x;
            var row = 2 * point.z + point.x;
            var x = radius * 3 / 2 * col + fieldCenter.X;
            var y = radius * Math.Sqrt(3) / 2 * row + fieldCenter.Y;
            var center = new Point((int)x, (int)y);
            drawHexagon(center, radius, pen, color, graphics);

            TextFormatFlags flags = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.WordBreak;
            var titleWidth = (int)(2 * radius);
            var titleHeight = (int)(2 * radius * Math.Sqrt(3) / 2);
            var titleX = (center.X - titleWidth / 2);
            var titleY = (center.Y - titleHeight / 2);
            var titleBounds = new Rectangle(titleX, titleY, titleWidth, titleHeight);

            TextRenderer.DrawText(graphics, title, font, titleBounds, Color.Black, flags);
        }


        public static void drawHexagon(Point center, float radius, Pen pen, Color color, Graphics graphics)
        {
            var shape = new Point[6];
            for (int a = 0; a < 6; a++)
            {
                var x = center.X + radius * (float)Math.Cos(a * 60 * Math.PI / 180f);
                var y = center.Y + radius * (float)Math.Sin(a * 60 * Math.PI / 180f);
                shape[a] = new Point((int)x, (int)y);
            }

            SolidBrush brush = new SolidBrush(color);
            graphics.FillPolygon(brush, shape);
            graphics.DrawPolygon(pen, shape);
        }
    }
}
