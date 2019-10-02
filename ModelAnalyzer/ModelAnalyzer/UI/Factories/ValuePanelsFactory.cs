using System;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;

using ModelAnalyzer.Services;
using ModelAnalyzer.Parameters;
using ModelAnalyzer.Parameters.Topology;

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
            if (p is RoutesMap)
                AddRoutesMap(p as RoutesMap, panel, clickHandler);
            if (p is PairsArrayParameter)
                AddPairsArray(p as PairsArrayParameter, panel, clickHandler);

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

        private void AddRoutesMap(RoutesMap p, Panel panel, EventHandler clickHandler)
        {
            Label value = new Label()
            {
                Text = string.Format("{0} путей", p.GetRoutesAmount()),
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = UIConstants.labelsPadding,
            };
            value.Click += clickHandler;

            panel.Controls.Add(value);
        }

        private void AddPairsArray(PairsArrayParameter p, Panel panel, EventHandler clickHandler)
        {
            Label value = new Label()
            {
                Text = string.Format("{0} пар", p.GetValue().Count()),
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = UIConstants.labelsPadding,
            };
            value.Click += clickHandler;

            panel.Controls.Add(value);
        }
    }
}
