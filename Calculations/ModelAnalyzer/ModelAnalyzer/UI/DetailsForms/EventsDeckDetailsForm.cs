using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using ModelAnalyzer.Services;
using ModelAnalyzer.DataModels;
using ModelAnalyzer.Parameters;

namespace ModelAnalyzer.UI
{
    public partial class EventsDeckDetailsForm : Form, IParameterDetailsForm
    {
        const int rowHeight = 35;
        const int relationWidth = 40;
        const int usabilityWidth = 40;
        const int indexWidth = 30;
        private readonly string emptyStub = "-";
        private readonly string issueItemPrefix = "- ";
        private readonly int maxRadius = 4;

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

        public EventsDeckDetailsForm()
        {
            InitializeComponent();
            predefinedControls = new List<Control>(DeckTable.Controls.Cast<Control>());
        }

        public void SetParameter(Parameter parameter, ParameterValidationReport validation)
        {
            if (!(parameter is DeckParameter))
                return;

            deck = ((DeckParameter)parameter).deck;
            var issues = parameter.calculationReport.GetIssues();

            issuesLabel.Text = "";
            foreach (string issue in issues)
            {
                var prefix = issues.Count > 1 ? issueItemPrefix : "";
                issuesLabel.Text += prefix + issue;
                if (issue != issues.Last())
                    issuesLabel.Text += Environment.NewLine;
            }

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

            if (cards == null)
                return;

            DeckTable.RowCount = cards.Count;
            DeckTable.Controls.AddRange(predefinedControls.ToArray());

            foreach (EventCard card in cards)
            {
                var relations = card.relations.OrderBy(r => r.position);
                var index = cards.FindIndex(c => ReferenceEquals(c, card)) + 1;

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
                DeckTable.Controls.Add(LabelForInt(card.stabilityBonus));
                DeckTable.Controls.Add(LabelForInt(card.miningBonus));
                DeckTable.Controls.Add(LabelForFloat(card.usability));
                DeckTable.Controls.Add(LabelForBranchPoints(card.branchPoints.success));
                DeckTable.Controls.Add(LabelForBranchPoints(card.branchPoints.failed));
                DeckTable.Controls.Add(LabelForInt(card.constraints.minPhase));
                DeckTable.Controls.Add(LabelForRadiusesConstraint(card.constraints.unavailableRadiuses));
                DeckTable.Controls.Add(LabelForInt(card.constraints.minStability));
                DeckTable.Controls.Add(LabelForFloat(card.weight));
                DeckTable.Controls.Add(LabelForFloat(card.positiveRealisationChance));
                DeckTable.Controls.Add(LabelForString(card.comment));
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

        private Label LabelForBranchPoints(List<BranchPoint> branchPoints)
        {
            string text = "";
            foreach (var branchPoint in branchPoints)
            {
                var sign = branchPoint.point > 0 ? "+" : "-";
                var points = Math.Abs(branchPoint.point);
                text += string.Format("{0}{1}({2})", sign, points, branchPoint.branch);
                if (branchPoint != branchPoints.Last())
                    text += " , ";
            }
            text = text == "" ? "-" : text;

            return new Label()
            {
                Text = text,
                MinimumSize = new Size(usabilityWidth, rowHeight),
                MaximumSize = new Size(usabilityWidth, rowHeight),
                TextAlign = ContentAlignment.MiddleCenter
            };
        }

        private Label LabelForRadiusesConstraint(List<int> unavailable)
        {
            string text = "";
            if (unavailable.Count() != 0)
                for (int i = 1; i <= maxRadius; i++)
                    text += unavailable.Contains(i) ? "- " : "+ ";
            else
                text = "-";
            text.TrimEnd();

            return new Label()
            {
                Text = text,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill
            };
        }

        private Label LabelForString(string value)
        {
            return new Label()
            {
                Text = value,
                TextAlign = ContentAlignment.MiddleLeft,
                Dock = DockStyle.Fill
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
            order = c => c.stabilityBonus;
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

        private void successBPLabel_Click(object sender, EventArgs e)
        {
            order = c => c.branchPoints.success.Count();
            reverse = !reverse;
            UpdateCardsTable();
        }

        private void failedBPLabel_Click(object sender, EventArgs e)
        {
            order = c => c.branchPoints.failed.Count();
            reverse = !reverse;
            UpdateCardsTable();
        }

        private void minPhaseLabel_Click(object sender, EventArgs e)
        {
            order = c => c.constraints.minPhase;
            reverse = !reverse;
            UpdateCardsTable();
        }

        private void minRadiusLabel_Click(object sender, EventArgs e)
        {
            order = c => c.constraints.unavailableRadiuses.Count();
            reverse = !reverse;
            UpdateCardsTable();
        }

        private void minStavilityLabel_Click(object sender, EventArgs e)
        {
            order = c => c.constraints.minStability;
            reverse = !reverse;
            UpdateCardsTable();
        }

        private void GenerateXMLButton_Click(object sender, EventArgs e)
        {
            if (saveXMLDialog.ShowDialog() == DialogResult.Cancel)
                return;
            
            string filename = saveXMLDialog.FileName;

            DeckXMLGenerator.GenerateXML(deck, filename);
        }

        private void PRClabel_Click(object sender, EventArgs e)
        {
            order = c => c.positiveRealisationChance;
            reverse = !reverse;
            UpdateCardsTable();
        }
    }
}
