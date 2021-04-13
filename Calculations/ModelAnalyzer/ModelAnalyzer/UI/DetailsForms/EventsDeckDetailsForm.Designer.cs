namespace ModelAnalyzer.UI.DetailsForms
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
            this.deckIndexLabel = new System.Windows.Forms.Label();
            this.deckRelationsLabel = new System.Windows.Forms.Label();
            this.deckWeightLabel = new System.Windows.Forms.Label();
            this.minPhaseLabel = new System.Windows.Forms.Label();
            this.minRadius = new System.Windows.Forms.Label();
            this.failedBPLabel = new System.Windows.Forms.Label();
            this.successBPLabel = new System.Windows.Forms.Label();
            this.deckUsabilityLabel = new System.Windows.Forms.Label();
            this.deckMBLabel = new System.Windows.Forms.Label();
            this.deckArtifactLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
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
            this.DeckTable.ColumnCount = 17;
            this.DeckTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.DeckTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.DeckTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.DeckTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.DeckTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.DeckTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.DeckTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.DeckTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.DeckTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 41F));
            this.DeckTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.DeckTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.DeckTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.DeckTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 37F));
            this.DeckTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.DeckTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 57F));
            this.DeckTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 52F));
            this.DeckTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 367F));
            this.DeckTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 143F));
            this.DeckTable.Controls.Add(this.deckIndexLabel, 0, 0);
            this.DeckTable.Controls.Add(this.deckRelationsLabel, 1, 0);
            this.DeckTable.Controls.Add(this.deckWeightLabel, 15, 0);
            this.DeckTable.Controls.Add(this.minPhaseLabel, 13, 0);
            this.DeckTable.Controls.Add(this.minRadius, 14, 0);
            this.DeckTable.Controls.Add(this.failedBPLabel, 12, 0);
            this.DeckTable.Controls.Add(this.successBPLabel, 11, 0);
            this.DeckTable.Controls.Add(this.deckUsabilityLabel, 10, 0);
            this.DeckTable.Controls.Add(this.deckMBLabel, 9, 0);
            this.DeckTable.Controls.Add(this.deckArtifactLabel, 8, 0);
            this.DeckTable.Controls.Add(this.label1, 7, 0);
            this.DeckTable.Controls.Add(this.deckCommentLabel, 16, 0);
            this.DeckTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DeckTable.Location = new System.Drawing.Point(0, 23);
            this.DeckTable.Name = "DeckTable";
            this.DeckTable.RowCount = 2;
            this.DeckTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.DeckTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.DeckTable.Size = new System.Drawing.Size(1047, 279);
            this.DeckTable.TabIndex = 0;
            // 
            // deckIndexLabel
            // 
            this.deckIndexLabel.AutoSize = true;
            this.deckIndexLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.deckIndexLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.deckIndexLabel.Location = new System.Drawing.Point(3, 10);
            this.deckIndexLabel.Margin = new System.Windows.Forms.Padding(3, 10, 3, 10);
            this.deckIndexLabel.Name = "deckIndexLabel";
            this.deckIndexLabel.Size = new System.Drawing.Size(24, 119);
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
            this.deckRelationsLabel.Size = new System.Drawing.Size(234, 119);
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
            this.deckWeightLabel.Location = new System.Drawing.Point(610, 10);
            this.deckWeightLabel.Margin = new System.Windows.Forms.Padding(3, 10, 3, 10);
            this.deckWeightLabel.Name = "deckWeightLabel";
            this.deckWeightLabel.Size = new System.Drawing.Size(46, 119);
            this.deckWeightLabel.TabIndex = 7;
            this.deckWeightLabel.Text = "Вес";
            this.deckWeightLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.deckWeightLabel.Click += new System.EventHandler(this.deckWeightLabel_Click);
            // 
            // minPhaseLabel
            // 
            this.minPhaseLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.minPhaseLabel.AutoSize = true;
            this.minPhaseLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.minPhaseLabel.Location = new System.Drawing.Point(505, 10);
            this.minPhaseLabel.Margin = new System.Windows.Forms.Padding(3, 10, 3, 10);
            this.minPhaseLabel.Name = "minPhaseLabel";
            this.minPhaseLabel.Size = new System.Drawing.Size(42, 119);
            this.minPhaseLabel.TabIndex = 12;
            this.minPhaseLabel.Text = "Мин Фаза";
            this.minPhaseLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.minPhaseLabel.Click += new System.EventHandler(this.minPhaseLabel_Click);
            // 
            // minRadius
            // 
            this.minRadius.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.minRadius.AutoSize = true;
            this.minRadius.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.minRadius.Location = new System.Drawing.Point(553, 10);
            this.minRadius.Margin = new System.Windows.Forms.Padding(3, 10, 3, 10);
            this.minRadius.Name = "minRadius";
            this.minRadius.Size = new System.Drawing.Size(51, 119);
            this.minRadius.TabIndex = 15;
            this.minRadius.Text = "Мин Радиус";
            this.minRadius.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.minRadius.Click += new System.EventHandler(this.minRadiusLabel_Click);
            // 
            // failedBPLabel
            // 
            this.failedBPLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.failedBPLabel.AutoSize = true;
            this.failedBPLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.failedBPLabel.Location = new System.Drawing.Point(468, 10);
            this.failedBPLabel.Margin = new System.Windows.Forms.Padding(3, 10, 3, 10);
            this.failedBPLabel.Name = "failedBPLabel";
            this.failedBPLabel.Size = new System.Drawing.Size(31, 119);
            this.failedBPLabel.TabIndex = 11;
            this.failedBPLabel.Text = "-";
            this.failedBPLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.failedBPLabel.Click += new System.EventHandler(this.failedBPLabel_Click);
            // 
            // successBPLabel
            // 
            this.successBPLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.successBPLabel.AutoSize = true;
            this.successBPLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.successBPLabel.Location = new System.Drawing.Point(432, 10);
            this.successBPLabel.Margin = new System.Windows.Forms.Padding(3, 10, 3, 10);
            this.successBPLabel.Name = "successBPLabel";
            this.successBPLabel.Size = new System.Drawing.Size(30, 119);
            this.successBPLabel.TabIndex = 16;
            this.successBPLabel.Text = "+";
            this.successBPLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.successBPLabel.Click += new System.EventHandler(this.successBPLabel_Click);
            // 
            // deckUsabilityLabel
            // 
            this.deckUsabilityLabel.AutoSize = true;
            this.deckUsabilityLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.deckUsabilityLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.deckUsabilityLabel.Location = new System.Drawing.Point(397, 10);
            this.deckUsabilityLabel.Margin = new System.Windows.Forms.Padding(3, 10, 3, 10);
            this.deckUsabilityLabel.Name = "deckUsabilityLabel";
            this.deckUsabilityLabel.Size = new System.Drawing.Size(29, 119);
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
            this.deckMBLabel.Location = new System.Drawing.Point(359, 10);
            this.deckMBLabel.Margin = new System.Windows.Forms.Padding(3, 10, 3, 10);
            this.deckMBLabel.Name = "deckMBLabel";
            this.deckMBLabel.Size = new System.Drawing.Size(32, 119);
            this.deckMBLabel.TabIndex = 4;
            this.deckMBLabel.Text = "MB";
            this.deckMBLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.deckMBLabel.Click += new System.EventHandler(this.deckMBLabel_Click);
            // 
            // deckArtifactLabel
            // 
            this.deckArtifactLabel.AutoSize = true;
            this.deckArtifactLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.deckArtifactLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.deckArtifactLabel.Location = new System.Drawing.Point(318, 10);
            this.deckArtifactLabel.Margin = new System.Windows.Forms.Padding(3, 10, 3, 10);
            this.deckArtifactLabel.Name = "deckArtifactLabel";
            this.deckArtifactLabel.Size = new System.Drawing.Size(35, 119);
            this.deckArtifactLabel.TabIndex = 2;
            this.deckArtifactLabel.Text = "Aрт";
            this.deckArtifactLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.deckArtifactLabel.Click += new System.EventHandler(this.deckArtifactLabel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(273, 10);
            this.label1.Margin = new System.Windows.Forms.Padding(3, 10, 3, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 119);
            this.label1.TabIndex = 17;
            this.label1.Text = "Сопр";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label1.Click += new System.EventHandler(this.pairedLabel_Click);
            // 
            // deckCommentLabel
            // 
            this.deckCommentLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.deckCommentLabel.AutoSize = true;
            this.deckCommentLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.deckCommentLabel.Location = new System.Drawing.Point(662, 10);
            this.deckCommentLabel.Margin = new System.Windows.Forms.Padding(3, 10, 3, 10);
            this.deckCommentLabel.Name = "deckCommentLabel";
            this.deckCommentLabel.Size = new System.Drawing.Size(382, 119);
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
            this.issuesPamel.Size = new System.Drawing.Size(1047, 23);
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
            this.panel1.Location = new System.Drawing.Point(0, 302);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1047, 40);
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
            this.ClientSize = new System.Drawing.Size(1047, 342);
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
        private System.Windows.Forms.Label deckArtifactLabel;
        private System.Windows.Forms.Label deckRelationsLabel;
        private System.Windows.Forms.Panel issuesPamel;
        private System.Windows.Forms.Label issuesLabel;
        private System.Windows.Forms.Label deckWeightLabel;
        private System.Windows.Forms.Label deckCommentLabel;
        private System.Windows.Forms.Label failedBPLabel;
        private System.Windows.Forms.Label minPhaseLabel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button GenerateXMLButton;
        private System.Windows.Forms.SaveFileDialog saveXMLDialog;
        private System.Windows.Forms.Label minRadius;
        private System.Windows.Forms.Label successBPLabel;
        private System.Windows.Forms.Label label1;
    }
}