using System;
using System.Windows.Forms;
using System.Drawing;

using ModelAnalyzer.Parameters;
using ModelAnalyzer.Services;

namespace ModelAnalyzer.UI.Factories
{
    class TimingRowsFactory
    {
        private readonly ComponentsFactory components = new ComponentsFactory();

        private readonly Color rowBack = Color.FromArgb(250, 250, 250);
        private readonly int rowHeight = 35;
        private readonly int valueWidth = 150;

        public Panel RowForReport(OperationReport report)
        {
            if (report is ParameterCalculationReport pcr)
            {
                var indicator = components.TypeIndicator(pcr.parameter.type);
                return RowFor(pcr.parameter.title, pcr.duration, indicator);
            }
            if (report is ModuleCalculationReport mcr)
            {
                return RowFor(mcr.moduleTitle, report.duration);
            }
            throw new Exception("Requested timing row for unsuppoter report tipe");
        }

        public Panel RowFor(string title, double value, Panel indicator = null)
        {
            Panel panel = components.RowPanel(rowBack, rowHeight);
            Label titleLabel = components.TitleLabel(title);

            var valueText = HumanRedeableMillisec(value);
            Label valueLabel = new Label
            {
                Text = valueText,
                Dock = DockStyle.Right,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = UIConstants.labelsPadding,
                Width = valueWidth
            };

            panel.Controls.Add(titleLabel);
            if (indicator != null)
                panel.Controls.Add(indicator);
            panel.Controls.Add(valueLabel);

            return panel;
        }

        private string HumanRedeableMillisec(double millisec)
        {
            TimeSpan t = TimeSpan.FromMilliseconds(millisec);
            return string.Format("{0:D2}:{1:D2}:{2:D2}:{3:D3}",
                                    t.Hours,
                                    t.Minutes,
                                    t.Seconds,
                                    t.Milliseconds);
        }
    }
}
