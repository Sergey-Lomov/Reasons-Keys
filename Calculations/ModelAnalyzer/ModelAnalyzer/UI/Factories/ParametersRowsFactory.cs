using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

using ModelAnalyzer.Parameters;

namespace ModelAnalyzer.UI.Factories
{
    class ParametersRowsFactory
    {
        private ComponentsFactory components = new ComponentsFactory();
        private ValuePanelsFactory valuePanels = new ValuePanelsFactory();

        private Dictionary<ParameterType, string> typesTitles = new Dictionary<ParameterType, string>();

        private readonly Color rowBack = Color.FromArgb(250, 250, 250);
        private readonly int rowHeight = 35;
        private readonly int valueWidth = 135;

        public ParametersRowsFactory ()
        {
            typesTitles.Add(ParameterType.In, "Входящие");
            typesTitles.Add(ParameterType.Out, "Исходящие");
            typesTitles.Add(ParameterType.Inner, "Внутренние");
            typesTitles.Add(ParameterType.Indicator, "Индикаторы");
        }

        public Panel HeaderForParameterType(ParameterType type)
        {
            var panel = components.RowPanel(rowBack, UIConstants.headerRowHeight);
            var titleLabel = components.HeaderTitleLabel(typesTitles[type]);
            panel.Controls.Add(titleLabel);

            return panel;
        }

        public Panel RowForParameter(Parameter parameter, IParameterRowDelegate rowDelegate, ParameterValidationReport validation)
        {
            Panel panel = components.RowPanel(rowBack, rowHeight);

            Label title = components.TitleLabel(parameter.title);

            EventHandler valueClickHandler = null;
            if (rowDelegate != null)
            {
                title.Click += (sender, e) => rowDelegate.HandleTitleClick(parameter);
                valueClickHandler = (sender, e) => rowDelegate.HandleValueClick(parameter);
            }
            Panel value = valuePanels.ValuePanel(parameter, false, valueClickHandler);

            value.Dock = DockStyle.Right;
            value.Width = valueWidth;

            panel.Controls.Add(title);
            panel.Controls.Add(components.TypeIndicator(parameter.type));
            panel.Controls.Add(value);

            if (validation != null)
                panel.Controls.Add(components.IssuesIndicator(parameter, validation));

            return panel;
        }
    }
}
