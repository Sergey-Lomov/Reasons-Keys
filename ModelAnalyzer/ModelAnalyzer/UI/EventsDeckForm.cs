using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using ModelAnalyzer.DataModels;
using ModelAnalyzer.Parameters.Events;

namespace ModelAnalyzer.UI
{
    public partial class EventsDeckForm : Form, IParameterDetailsForm
    {
        const int rowHeight = 35;
        const int relationWidth = 40;
        const int usabilityWidth = 40;
        const int indexWidth = 30;
        const string emptyStub = "-";

        private List<Control> predefinedControls;
        private List<EventCard> deck;
        private Func<EventCard, IComparable> order = null;
        private bool reverse = false;

        readonly Dictionary<RelationType, string> relationsTitles = new Dictionary<RelationType, string>()
        {
            { RelationType.reason, "П" },
            { RelationType.paired_reason, "С-П" },
            { RelationType.blocker, "Б" }
        };

        public EventsDeckForm()
        {
            InitializeComponent();
            predefinedControls = new List<Control>(DeckTable.Controls.Cast<Control>());
        }

        public void SetParameter(Parameter parameter, ParameterValidationReport validation)
        {
            if (!(parameter is EventsDeck))
                return;

            deck = ((EventsDeck)parameter).deck;
            UpdateCardsTable();
        }

        private void UpdateCardsTable ()
        {
            var cards = order != null ? deck.OrderBy(order).ToList() : deck;
            if (reverse)
                cards.Reverse();

            DeckTable.Controls.Clear();
            DeckTable.RowStyles.Clear();
            DeckTable.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            DeckTable.ColumnStyles.Clear();
            DeckTable.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));

            DeckTable.RowCount = cards.Count;
            DeckTable.Controls.AddRange(predefinedControls.ToArray());

            foreach (EventCard card in cards)
            {
                var relations = card.relations.OrderBy(r => r.position);
                var index = cards.IndexOf(card) + 1;
                DeckTable.Controls.Add(IndexLabel(index));

                for (int i = 0; i < EventRelation.MaxRelationPosition; i++)
                {
                    var label = EmptyLabel();
                    foreach (EventRelation relation in relations)
                        if (relation.position == i)
                            label = LabelForRelation(relation);

                    DeckTable.Controls.Add(label);
                }

                DeckTable.Controls.Add(LabelForBool(card.provideArtifact));
                DeckTable.Controls.Add(LabelForInt(card.stabilityIncrement));
                DeckTable.Controls.Add(LabelForInt(card.miningBonus));
                DeckTable.Controls.Add(LabelForFloat(card.usability));
                DeckTable.Controls.Add(LabelForFloat(card.weight));
            }
        }

        private Label LabelForRelation(EventRelation relation)
        {
            return new Label()
            {
                Text = relationsTitles[relation.type],
                MinimumSize = new Size(relationWidth, rowHeight),
                MaximumSize = new Size(relationWidth, rowHeight),
                TextAlign = ContentAlignment.MiddleCenter
            };
        }

        private Label LabelForFloat(float value)
        {
            return new Label()
            {
                Text = string.Format("{0:0.##}", value),
                MinimumSize = new Size(usabilityWidth, rowHeight),
                MaximumSize = new Size(usabilityWidth, rowHeight),
                TextAlign = ContentAlignment.MiddleCenter
            };
        }

        private Label LabelForInt(int value)
        {
            return new Label()
            {
                Text = string.Format("{0}", value),
                MinimumSize = new Size(usabilityWidth, rowHeight),
                MaximumSize = new Size(usabilityWidth, rowHeight),
                TextAlign = ContentAlignment.MiddleCenter
            };
        }

        private Label LabelForBool(bool value)
        {
            return new Label()
            {
                Text = value ? "+" : "-",
                MinimumSize = new Size(usabilityWidth, rowHeight),
                MaximumSize = new Size(usabilityWidth, rowHeight),
                TextAlign = ContentAlignment.MiddleCenter
            };
        }

        private Label EmptyLabel()
        {
            return new Label()
            {
                Text = emptyStub,
                MinimumSize = new Size(relationWidth, rowHeight),
                MaximumSize = new Size(relationWidth, rowHeight),
                TextAlign = ContentAlignment.MiddleCenter
            };
        }

        private Label IndexLabel(int index)
        {
            return new Label()
            {
                Text = string.Format("{0}",index),
                MinimumSize = new Size(indexWidth, rowHeight),
                MaximumSize = new Size(indexWidth, rowHeight),
                TextAlign = ContentAlignment.MiddleCenter
            };
        }

        private void deckArtifactLabel_Click(object sender, EventArgs e)
        {
            order = c => c.provideArtifact;
            reverse = !reverse;
            UpdateCardsTable();
        }

        private void deckSILabel_Click(object sender, EventArgs e)
        {
            order = c => c.stabilityIncrement;
            reverse = !reverse;
            UpdateCardsTable();
        }

        private void deckMBLabel_Click(object sender, EventArgs e)
        {
            order = c => c.miningBonus;
            reverse = !reverse;
            UpdateCardsTable();
        }

        private void deckUsabilityLabel_Click(object sender, EventArgs e)
        {
            order = c => c.usability;
            reverse = !reverse;
            UpdateCardsTable();
        }

        private void deckWeightLabel_Click(object sender, EventArgs e)
        {
            order = c => c.weight;
            reverse = !reverse;
            UpdateCardsTable();
        }

        private void deckIndexLabel_Click(object sender, EventArgs e)
        {
            order = null;
            reverse = false;
            UpdateCardsTable();
        }
    }
}
