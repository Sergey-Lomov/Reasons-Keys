﻿namespace ModelAnalyzer.UI
{
    partial class PairsArrayDetailsForm
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
            this.titleLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.detailsTitleLabel = new System.Windows.Forms.Label();
            this.valueTitleLabel = new System.Windows.Forms.Label();
            this.issuesTitleLabel = new System.Windows.Forms.Label();
            this.detailsLabel = new System.Windows.Forms.Label();
            this.issuesLabel = new System.Windows.Forms.Label();
            this.valueTable = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // titleLabel
            // 
            this.titleLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.titleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.titleLabel.Location = new System.Drawing.Point(0, 0);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(784, 32);
            this.titleLabel.TabIndex = 1;
            this.titleLabel.Text = "Title";
            this.titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoScroll = true;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.detailsTitleLabel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.valueTitleLabel, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.issuesTitleLabel, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.detailsLabel, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.issuesLabel, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.valueTable, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 32);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(784, 229);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // detailsTitleLabel
            // 
            this.detailsTitleLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.detailsTitleLabel.AutoSize = true;
            this.detailsTitleLabel.Location = new System.Drawing.Point(3, 0);
            this.detailsTitleLabel.Name = "detailsTitleLabel";
            this.detailsTitleLabel.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.detailsTitleLabel.Size = new System.Drawing.Size(84, 29);
            this.detailsTitleLabel.TabIndex = 0;
            this.detailsTitleLabel.Text = "Детали";
            this.detailsTitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // valueTitleLabel
            // 
            this.valueTitleLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.valueTitleLabel.AutoSize = true;
            this.valueTitleLabel.Location = new System.Drawing.Point(3, 29);
            this.valueTitleLabel.Name = "valueTitleLabel";
            this.valueTitleLabel.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.valueTitleLabel.Size = new System.Drawing.Size(84, 46);
            this.valueTitleLabel.TabIndex = 1;
            this.valueTitleLabel.Text = "Значение";
            this.valueTitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // issuesTitleLabel
            // 
            this.issuesTitleLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.issuesTitleLabel.AutoSize = true;
            this.issuesTitleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.issuesTitleLabel.ForeColor = System.Drawing.Color.Red;
            this.issuesTitleLabel.Location = new System.Drawing.Point(3, 75);
            this.issuesTitleLabel.Name = "issuesTitleLabel";
            this.issuesTitleLabel.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.issuesTitleLabel.Size = new System.Drawing.Size(84, 29);
            this.issuesTitleLabel.TabIndex = 3;
            this.issuesTitleLabel.Text = "Проблемы";
            this.issuesTitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // detailsLabel
            // 
            this.detailsLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.detailsLabel.AutoSize = true;
            this.detailsLabel.Location = new System.Drawing.Point(93, 0);
            this.detailsLabel.Name = "detailsLabel";
            this.detailsLabel.Padding = new System.Windows.Forms.Padding(0, 8, 0, 8);
            this.detailsLabel.Size = new System.Drawing.Size(688, 29);
            this.detailsLabel.TabIndex = 4;
            this.detailsLabel.Text = "Details";
            this.detailsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // issuesLabel
            // 
            this.issuesLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.issuesLabel.AutoSize = true;
            this.issuesLabel.Location = new System.Drawing.Point(93, 75);
            this.issuesLabel.Name = "issuesLabel";
            this.issuesLabel.Padding = new System.Windows.Forms.Padding(0, 8, 0, 8);
            this.issuesLabel.Size = new System.Drawing.Size(688, 29);
            this.issuesLabel.TabIndex = 7;
            this.issuesLabel.Text = "Issues";
            this.issuesLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // valueTable
            // 
            this.valueTable.AutoSize = true;
            this.valueTable.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.valueTable.ColumnCount = 2;
            this.valueTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.valueTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.valueTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.valueTable.Location = new System.Drawing.Point(93, 32);
            this.valueTable.Name = "valueTable";
            this.valueTable.RowCount = 2;
            this.valueTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.valueTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.valueTable.Size = new System.Drawing.Size(688, 40);
            this.valueTable.TabIndex = 8;
            // 
            // PairsArrayDetailsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 261);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.titleLabel);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(800, 300);
            this.Name = "PairsArrayDetailsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "PairsArrayDetailsForm";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label detailsTitleLabel;
        private System.Windows.Forms.Label valueTitleLabel;
        private System.Windows.Forms.Label issuesTitleLabel;
        private System.Windows.Forms.Label detailsLabel;
        private System.Windows.Forms.Label issuesLabel;
        private System.Windows.Forms.TableLayoutPanel valueTable;
    }
}