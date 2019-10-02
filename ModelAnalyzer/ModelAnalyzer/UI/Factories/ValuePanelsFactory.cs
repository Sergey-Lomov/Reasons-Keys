using System;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;

using ModelAnalyzer.Services;
using ModelAnalyzer.Parameters;

namespace ModelAnalyzer.UI.Factories
{
    class ValuePanelsFactory
    {
        public Panel ValuePanel(Parameter p, EventHandler clickHandler)
        {
            var panel = new Panel();
            panel.Click += clickHandler;

            if (p is FloatSingleParameter)
                AddFloatSingle(p as FloatSingleParameter, panel, clickHandler);
            if (p is FloatArrayParameter)
                AddFloatArray(p as FloatArrayParameter, panel, clickHandler);
            if (p is DeckParameter)
                AddDeck(p as DeckParameter, panel, clickHandler);

            return panel;
        }

        private void AddFloatSingle(FloatSingleParameter p, Panel panel, EventHandler clickHandler)
        {
            Label value = new Label()
            {
                Text = FloatStringConverter.FloatToString(p.GetValue(), p.fractionalDigits),
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = UIConstants.labelsPadding,
            };
            value.Click += clickHandler;

            panel.Controls.Add(value);
        }

        private void AddFloatArray(FloatArrayParameter p, Panel panel, EventHandler clickHandler)
        {
            Label value = new Label()
            {
                Text = FloatStringConverter.ListToString(p.GetValue(), p.fractionalDigits),
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = UIConstants.labelsPadding,
            };
            value.Click += clickHandler;

            panel.Controls.Add(value);
        }

        private void AddDeck(DeckParameter p, Panel panel, EventHandler clickHandler)
        {
            Label value = new Label()
            {
                Text = string.Format("{0} карт", p.deck.Count()),
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = UIConstants.labelsPadding,
            };
            value.Click += clickHandler;

            panel.Controls.Add(value);
        }
    }
}
