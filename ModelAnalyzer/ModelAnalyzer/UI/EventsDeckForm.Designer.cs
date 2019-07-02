namespace ModelAnalyzer.UI
{
    partial class EventsDeckForm
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
            this.deckUsabilityLabel = new System.Windows.Forms.Label();
            this.deckMBLabel = new System.Windows.Forms.Label();
            this.deckSILabel = new System.Windows.Forms.Label();
            this.deckArtifactLabel = new System.Windows.Forms.Label();
            this.deckIndexLabel = new System.Windows.Forms.Label();
            this.deckRelationsLabel = new System.Windows.Forms.Label();
            this.deckWeightLabel = new System.Windows.Forms.Label();
            this.DeckTable.SuspendLayout();
            this.SuspendLayout();
            // 
            // DeckTable
            // 
            this.DeckTable.AutoScroll = true;
            this.DeckTable.AutoSize = true;
            this.DeckTable.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.DeckTable.ColumnCount = 12;
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
            this.DeckTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.DeckTable.Controls.Add(this.deckUsabilityLabel, 10, 0);
            this.DeckTable.Controls.Add(this.deckMBLabel, 9, 0);
            this.DeckTable.Controls.Add(this.deckSILabel, 8, 0);
            this.DeckTable.Controls.Add(this.deckArtifactLabel, 7, 0);
            this.DeckTable.Controls.Add(this.deckIndexLabel, 0, 0);
            this.DeckTable.Controls.Add(this.deckRelationsLabel, 1, 0);
            this.DeckTable.Controls.Add(this.deckWeightLabel, 11, 0);
            this.DeckTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DeckTable.Location = new System.Drawing.Point(0, 0);
            this.DeckTable.Name = "DeckTable";
            this.DeckTable.RowCount = 2;
            this.DeckTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.DeckTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.DeckTable.Size = new System.Drawing.Size(566, 333);
            this.DeckTable.TabIndex = 0;
            // 
            // deckUsabilityLabel
            // 
            this.deckUsabilityLabel.AutoSize = true;
            this.deckUsabilityLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.deckUsabilityLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.deckUsabilityLabel.Location = new System.Drawing.Point(393, 10);
            this.deckUsabilityLabel.Margin = new System.Windows.Forms.Padding(3, 10, 3, 10);
            this.deckUsabilityLabel.Name = "deckUsabilityLabel";
            this.deckUsabilityLabel.Size = new System.Drawing.Size(34, 146);
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
            this.deckMBLabel.Size = new System.Drawing.Size(34, 146);
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
            this.deckSILabel.Size = new System.Drawing.Size(34, 146);
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
            this.deckArtifactLabel.Size = new System.Drawing.Size(34, 146);
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
            this.deckIndexLabel.Size = new System.Drawing.Size(24, 146);
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
            this.deckRelationsLabel.Size = new System.Drawing.Size(234, 146);
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
            this.deckWeightLabel.Location = new System.Drawing.Point(433, 10);
            this.deckWeightLabel.Margin = new System.Windows.Forms.Padding(3, 10, 3, 10);
            this.deckWeightLabel.Name = "deckWeightLabel";
            this.deckWeightLabel.Size = new System.Drawing.Size(130, 146);
            this.deckWeightLabel.TabIndex = 7;
            this.deckWeightLabel.Text = "Вес";
            this.deckWeightLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.deckWeightLabel.Click += new System.EventHandler(this.deckWeightLabel_Click);
            // 
            // EventsDeckForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(566, 333);
            this.Controls.Add(this.DeckTable);
            this.Name = "EventsDeckForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "EventsDeckForm";
            this.DeckTable.ResumeLayout(false);
            this.DeckTable.PerformLayout();
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
        private System.Windows.Forms.Label deckWeightLabel;
    }
}