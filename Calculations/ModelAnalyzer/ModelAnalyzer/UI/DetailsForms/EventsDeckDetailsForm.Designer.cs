namespace ModelAnalyzer.UI
{
    partial class EventsDeckDetailsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.DeckTable = new System.Windows.Forms.TableLayoutPanel();
            this.PRClabel = new System.Windows.Forms.Label();
            this.deckUsabilityLabel = new System.Windows.Forms.Label();
            this.deckMBLabel = new System.Windows.Forms.Label();
            this.deckSILabel = new System.Windows.Forms.Label();
            this.deckArtifactLabel = new System.Windows.Forms.Label();
            this.deckIndexLabel = new System.Windows.Forms.Label();
            this.deckRelationsLabel = new System.Windows.Forms.Label();
            this.deckWeightLabel = new System.Windows.Forms.Label();
            this.successBPLabel = new System.Windows.Forms.Label();
            this.minRadiusLabel = new System.Windows.Forms.Label();
            this.failedBPLabel = new System.Windows.Forms.Label();
            this.minPhaseLabel = new System.Windows.Forms.Label();
            this.minStavilityLabel = new System.Windows.Forms.Label();
            this.deckCommentLabel = new System.Windows.Forms.Label();
            this.issuesPamel = new System.Windows.Forms.Panel();
            this.issuesLabel = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.GenerateXMLButton = new System.Windows.Forms.Button();
            this.saveXMLDialog = new System.Windows.Forms.SaveFileDialog();
            this.DeckTable.SuspendLayout();
            this.issuesPamel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // DeckTable
            // 
            this.DeckTable.AutoScroll = true;
            this.DeckTable.AutoSize = true;
            this.DeckTable.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.DeckTable.ColumnCount = 19;
            this.DeckTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.DeckTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.DeckTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.DeckTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.DeckTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.DeckTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.DeckTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.DeckTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.DeckTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.DeckTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.DeckTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.DeckTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.DeckTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.DeckTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.DeckTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.DeckTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.DeckTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.DeckTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 47F));
            this.DeckTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 284F));
            this.DeckTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.DeckTable.Controls.Add(this.PRClabel, 17, 0);
            this.DeckTable.Controls.Add(this.deckUsabilityLabel, 10, 0);
            this.DeckTable.Controls.Add(this.deckMBLabel, 9, 0);
            this.DeckTable.Controls.Add(this.deckSILabel, 8, 0);
            this.DeckTable.Controls.Add(this.deckArtifactLabel, 7, 0);
            this.DeckTable.Controls.Add(this.deckIndexLabel, 0, 0);
            this.DeckTable.Controls.Add(this.deckRelationsLabel, 1, 0);
            this.DeckTable.Controls.Add(this.deckWeightLabel, 16, 0);
            this.DeckTable.Controls.Add(this.successBPLabel, 11, 0);
            this.DeckTable.Controls.Add(this.minRadiusLabel, 14, 0);
            this.DeckTable.Controls.Add(this.failedBPLabel, 12, 0);
            this.DeckTable.Controls.Add(this.minPhaseLabel, 13, 0);
            this.DeckTable.Controls.Add(this.minStavilityLabel, 15, 0);
            this.DeckTable.Controls.Add(this.deckCommentLabel, 18, 0);
            this.DeckTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DeckTable.Location = new System.Drawing.Point(0, 23);
            this.DeckTable.Name = "DeckTable";
            this.DeckTable.RowCount = 2;
            this.DeckTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.DeckTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.DeckTable.Size = new System.Drawing.Size(1051, 265);
            this.DeckTable.TabIndex = 0;
            // 
            // PRClabel
            // 
            this.PRClabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PRClabel.AutoSize = true;
            this.PRClabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.PRClabel.Location = new System.Drawing.Point(743, 10);
            this.PRClabel.Margin = new System.Windows.Forms.Padding(3, 10, 3, 10);
            this.PRClabel.Name = "PRClabel";
            this.PRClabel.Size = new System.Drawing.Size(41, 112);
            this.PRClabel.TabIndex = 14;
            this.PRClabel.Text = "PRC";
            this.PRClabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.PRClabel.Click += new System.EventHandler(this.PRClabel_Click);
            // 
            // deckUsabilityLabel
            // 
            this.deckUsabilityLabel.AutoSize = true;
            this.deckUsabilityLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.deckUsabilityLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.deckUsabilityLabel.Location = new System.Drawing.Point(393, 10);
            this.deckUsabilityLabel.Margin = new System.Windows.Forms.Padding(3, 10, 3, 10);
            this.deckUsabilityLabel.Name = "deckUsabilityLabel";
            this.deckUsabilityLabel.Size = new System.Drawing.Size(34, 112);
            this.deckUsabilityLabel.TabIndex = 5;
            this.deckUsabilityLabel.Text = "Us";
            this.deckUsabilityLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.deckUsabilityLabel.Click += new System.EventHandler(this.deckUsabilityLabel_Click);
            // 
            // deckMBLabel
            // 
            this.deckMBLabel.AutoSize = true;
            this.deckMBLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.deckMBLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.deckMBLabel.Location = new System.Drawing.Point(353, 10);
            this.deckMBLabel.Margin = new System.Windows.Forms.Padding(3, 10, 3, 10);
            this.deckMBLabel.Name = "deckMBLabel";
            this.deckMBLabel.Size = new System.Drawing.Size(34, 112);
            this.deckMBLabel.TabIndex = 4;
            this.deckMBLabel.Text = "MB";
            this.deckMBLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.deckMBLabel.Click += new System.EventHandler(this.deckMBLabel_Click);
            // 
            // deckSILabel
            // 
            this.deckSILabel.AutoSize = true;
            this.deckSILabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.deckSILabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.deckSILabel.Location = new System.Drawing.Point(313, 10);
            this.deckSILabel.Margin = new System.Windows.Forms.Padding(3, 10, 3, 10);
            this.deckSILabel.Name = "deckSILabel";
            this.deckSILabel.Size = new System.Drawing.Size(34, 112);
            this.deckSILabel.TabIndex = 3;
            this.deckSILabel.Text = "SI";
            this.deckSILabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.deckSILabel.Click += new System.EventHandler(this.deckSILabel_Click);
            // 
            // deckArtifactLabel
            // 
            this.deckArtifactLabel.AutoSize = true;
            this.deckArtifactLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.deckArtifactLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.deckArtifactLabel.Location = new System.Drawing.Point(273, 10);
            this.deckArtifactLabel.Margin = new System.Windows.Forms.Padding(3, 10, 3, 10);
            this.deckArtifactLabel.Name = "deckArtifactLabel";
            this.deckArtifactLabel.Size = new System.Drawing.Size(34, 112);
            this.deckArtifactLabel.TabIndex = 2;
            this.deckArtifactLabel.Text = "Art";
            this.deckArtifactLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.deckArtifactLabel.Click += new System.EventHandler(this.deckArtifactLabel_Click);
            // 
            // deckIndexLabel
            // 
            this.deckIndexLabel.AutoSize = true;
            this.deckIndexLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.deckIndexLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.deckIndexLabel.Location = new System.Drawing.Point(3, 10);
            this.deckIndexLabel.Margin = new System.Windows.Forms.Padding(3, 10, 3, 10);
            this.deckIndexLabel.Name = "deckIndexLabel";
            this.deckIndexLabel.Size = new System.Drawing.Size(24, 112);
            this.deckIndexLabel.TabIndex = 0;
            this.deckIndexLabel.Text = "#";
            this.deckIndexLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.deckIndexLabel.Click += new System.EventHandler(this.deckIndexLabel_Click);
            // 
            // deckRelationsLabel
            // 
            this.deckRelationsLabel.AutoSize = true;
            this.DeckTable.SetColumnSpan(this.deckRelationsLabel, 6);
            this.deckRelationsLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.deckRelationsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.deckRelationsLabel.Location = new System.Drawing.Point(33, 10);
            this.deckRelationsLabel.Margin = new System.Windows.Forms.Padding(3, 10, 3, 10);
            this.deckRelationsLabel.Name = "deckRelationsLabel";
            this.deckRelationsLabel.Size = new System.Drawing.Size(234, 112);
            this.deckRelationsLabel.TabIndex = 1;
            this.deckRelationsLabel.Text = "Связи";
            this.deckRelationsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // deckWeightLabel
            // 
            this.deckWeightLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.deckWeightLabel.AutoSize = true;
            this.deckWeightLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.deckWeightLabel.Location = new System.Drawing.Point(693, 10);
            this.deckWeightLabel.Margin = new System.Windows.Forms.Padding(3, 10, 3, 10);
            this.deckWeightLabel.Name = "deckWeightLabel";
            this.deckWeightLabel.Size = new System.Drawing.Size(44, 112);
            this.deckWeightLabel.TabIndex = 7;
            this.deckWeightLabel.Text = "Вес";
            this.deckWeightLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.deckWeightLabel.Click += new System.EventHandler(this.deckWeightLabel_Click);
            // 
            // successBPLabel
            // 
            this.successBPLabel.AutoSize = true;
            this.successBPLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.successBPLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.successBPLabel.Location = new System.Drawing.Point(433, 10);
            this.successBPLabel.Margin = new System.Windows.Forms.Padding(3, 10, 3, 10);
            this.successBPLabel.Name = "successBPLabel";
            this.successBPLabel.Size = new System.Drawing.Size(34, 112);
            this.successBPLabel.TabIndex = 8;
            this.successBPLabel.Text = "+";
            this.successBPLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.successBPLabel.Click += new System.EventHandler(this.successBPLabel_Click);
            // 
            // minRadiusLabel
            // 
            this.minRadiusLabel.AutoSize = true;
            this.minRadiusLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.minRadiusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.minRadiusLabel.Location = new System.Drawing.Point(573, 10);
            this.minRadiusLabel.Margin = new System.Windows.Forms.Padding(3, 10, 3, 10);
            this.minRadiusLabel.Name = "minRadiusLabel";
            this.minRadiusLabel.Size = new System.Drawing.Size(54, 112);
            this.minRadiusLabel.TabIndex = 9;
            this.minRadiusLabel.Text = "Мин Радиус";
            this.minRadiusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.minRadiusLabel.Click += new System.EventHandler(this.minRadiusLabel_Click);
            // 
            // failedBPLabel
            // 
            this.failedBPLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.failedBPLabel.AutoSize = true;
            this.failedBPLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.failedBPLabel.Location = new System.Drawing.Point(473, 10);
            this.failedBPLabel.Margin = new System.Windows.Forms.Padding(3, 10, 3, 10);
            this.failedBPLabel.Name = "failedBPLabel";
            this.failedBPLabel.Size = new System.Drawing.Size(34, 112);
            this.failedBPLabel.TabIndex = 11;
            this.failedBPLabel.Text = "-";
            this.failedBPLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.failedBPLabel.Click += new System.EventHandler(this.failedBPLabel_Click);
            // 
            // minPhaseLabel
            // 
            this.minPhaseLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.minPhaseLabel.AutoSize = true;
            this.minPhaseLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.minPhaseLabel.Location = new System.Drawing.Point(513, 10);
            this.minPhaseLabel.Margin = new System.Windows.Forms.Padding(3, 10, 3, 10);
            this.minPhaseLabel.Name = "minPhaseLabel";
            this.minPhaseLabel.Size = new System.Drawing.Size(54, 112);
            this.minPhaseLabel.TabIndex = 12;
            this.minPhaseLabel.Text = "Мин Фаза";
            this.minPhaseLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.minPhaseLabel.Click += new System.EventHandler(this.minPhaseLabel_Click);
            // 
            // minStavilityLabel
            // 
            this.minStavilityLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.minStavilityLabel.AutoSize = true;
            this.minStavilityLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.minStavilityLabel.Location = new System.Drawing.Point(633, 10);
            this.minStavilityLabel.Margin = new System.Windows.Forms.Padding(3, 10, 3, 10);
            this.minStavilityLabel.Name = "minStavilityLabel";
            this.minStavilityLabel.Size = new System.Drawing.Size(54, 112);
            this.minStavilityLabel.TabIndex = 13;
            this.minStavilityLabel.Text = "Мин Стаб.";
            this.minStavilityLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.minStavilityLabel.Click += new System.EventHandler(this.minStavilityLabel_Click);
            // 
            // deckCommentLabel
            // 
            this.deckCommentLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.deckCommentLabel.AutoSize = true;
            this.deckCommentLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.deckCommentLabel.Location = new System.Drawing.Point(790, 10);
            this.deckCommentLabel.Margin = new System.Windows.Forms.Padding(3, 10, 3, 10);
            this.deckCommentLabel.Name = "deckCommentLabel";
            this.deckCommentLabel.Size = new System.Drawing.Size(278, 112);
            this.deckCommentLabel.TabIndex = 10;
            this.deckCommentLabel.Text = "Комментарий";
            this.deckCommentLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // issuesPamel
            // 
            this.issuesPamel.AutoSize = true;
            this.issuesPamel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.issuesPamel.Controls.Add(this.issuesLabel);
            this.issuesPamel.Dock = System.Windows.Forms.DockStyle.Top;
            this.issuesPamel.Location = new System.Drawing.Point(0, 0);
            this.issuesPamel.Name = "issuesPamel";
            this.issuesPamel.Size = new System.Drawing.Size(1051, 23);
            this.issuesPamel.TabIndex = 1;
            // 
            // issuesLabel
            // 
            this.issuesLabel.AutoSize = true;
            this.issuesLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.issuesLabel.ForeColor = System.Drawing.Color.Red;
            this.issuesLabel.Location = new System.Drawing.Point(0, 0);
            this.issuesLabel.MaximumSize = new System.Drawing.Size(800, 0);
            this.issuesLabel.Name = "issuesLabel";
            this.issuesLabel.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.issuesLabel.Size = new System.Drawing.Size(37, 23);
            this.issuesLabel.TabIndex = 0;
            this.issuesLabel.Text = "Issues";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.GenerateXMLButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 288);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1051, 40);
            this.panel1.TabIndex = 2;
            // 
            // GenerateXMLButton
            // 
            this.GenerateXMLButton.Location = new System.Drawing.Point(6, 8);
            this.GenerateXMLButton.Name = "GenerateXMLButton";
            this.GenerateXMLButton.Size = new System.Drawing.Size(159, 23);
            this.GenerateXMLButton.TabIndex = 0;
            this.GenerateXMLButton.Text = "Сгенерировать XML колоды";
            this.GenerateXMLButton.UseVisualStyleBackColor = true;
            this.GenerateXMLButton.Click += new System.EventHandler(this.GenerateXMLButton_Click);
            // 
            // saveXMLDialog
            // 
            this.saveXMLDialog.DefaultExt = "xml";
            this.saveXMLDialog.FileName = "Deck.xml";
            this.saveXMLDialog.Filter = "XML File|*.xml";
            // 
            // EventsDeckDetailsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1051, 328);
            this.Controls.Add(this.DeckTable);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.issuesPamel);
            this.Name = "EventsDeckDetailsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "EventsDeckForm";
            this.DeckTable.ResumeLayout(false);
            this.DeckTable.PerformLayout();
            this.issuesPamel.ResumeLayout(false);
            this.issuesPamel.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel DeckTable;
        private System.Windows.Forms.Label deckIndexLabel;
        private System.Windows.Forms.Label deckUsabilityLabel;
        private System.Windows.Forms.Label deckMBLabel;
        private System.Windows.Forms.Label deckSILabel;
        private System.Windows.Forms.Label deckArtifactLabel;
        private System.Windows.Forms.Label deckRelationsLabel;
        private System.Windows.Forms.Label successBPLabel;
        private System.Windows.Forms.Panel issuesPamel;
        private System.Windows.Forms.Label issuesLabel;
        private System.Windows.Forms.Label deckWeightLabel;
        private System.Windows.Forms.Label minRadiusLabel;
        private System.Windows.Forms.Label deckCommentLabel;
        private System.Windows.Forms.Label failedBPLabel;
        private System.Windows.Forms.Label minPhaseLabel;
        private System.Windows.Forms.Label minStavilityLabel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button GenerateXMLButton;
        private System.Windows.Forms.SaveFileDialog saveXMLDialog;
        private System.Windows.Forms.Label PRClabel;
    }
}