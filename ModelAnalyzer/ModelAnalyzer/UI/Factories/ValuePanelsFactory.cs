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
        private readonly int unroundFractionalDigits = 3;

        public Panel ValuePanel(Parameter p, bool advanced = false, EventHandler clickHandler = null)
        {
            var panel = new Panel();
            panel.Click += clickHandler;

            if (p is FloatSingleParameter floatSingle)
                AddFloatSingle(floatSingle, panel, advanced, clickHandler);
            if (p is FloatArrayParameter floatArray)
                AddFloatArray(floatArray, panel, advanced, clickHandler);
            if (p is DeckParameter deck)
                AddDeck(deck, panel, advanced, clickHandler);
            if (p is RoutesMap routesMap)
                AddRoutesMap(routesMap, panel, advanced, clickHandler);
            if (p is PairsArrayParameter pairsArray)
                AddPairsArray(pairsArray, panel, advanced, clickHandler);

            return panel;
        }

        private void AddFloatSingle(FloatSingleParameter p, Panel panel, bool advanced, EventHandler clickHandler)
        {
            var valueText = FloatStringConverter.FloatToString(p.GetValue(), p.fractionalDigits);
            var valueDock = advanced ? DockStyle.Top : DockStyle.Fill;
            AddLabel(valueText, valueDock, panel, clickHandler);
            
            if (advanced)
            {
                var unroundValueText = FloatStringConverter.FloatToString(p.GetUnroundValue(), unroundFractionalDigits);
                AddLabel(unroundValueText, DockStyle.Bottom, panel, clickHandler);
            }
        }

        private void AddFloatArray(FloatArrayParameter p, Panel panel, bool advanced, EventHandler clickHandler)
        {
            var valueText = FloatStringConverter.ListToString(p.GetValue(), p.fractionalDigits);
            var valueDock = advanced ? DockStyle.Top : DockStyle.Fill;
            AddLabel(valueText, valueDock, panel, clickHandler);

            if (advanced)
            {
                var unroundValueText = FloatStringConverter.ListToString(p.GetUnroundValue(), unroundFractionalDigits);
                AddLabel(unroundValueText, DockStyle.Bottom, panel, clickHandler);
            }
        }

        private void AddDeck(DeckParameter p, Panel panel, bool advanced, EventHandler clickHandler)
        {
            var text = string.Format("{0} карт", p.deck.Count());
            AddLabel(text, DockStyle.Fill, panel, clickHandler);
        }

        private void AddRoutesMap(RoutesMap p, Panel panel, bool advanced, EventHandler clickHandler)
        {
            var text = string.Format("{0} путей", p.GetRoutesAmount());
            AddLabel(text, DockStyle.Fill, panel, clickHandler);
        }

        private void AddPairsArray(PairsArrayParameter p, Panel panel, bool advanced, EventHandler clickHandler)
        {
            var text = string.Format("{0} пар", p.GetValue().Count());
            AddLabel(text, DockStyle.Fill, panel, clickHandler);
        }

        private void AddLabel(string text, DockStyle dock, Panel panel, EventHandler clickHandler)
        {
            Label value = new Label()
            {
                Text = text,
                Dock = dock,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = UIConstants.labelsPadding,
            };
            value.Click += clickHandler;

            panel.Controls.Add(value);
        }
    }
}
