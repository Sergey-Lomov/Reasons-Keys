namespace ModelAnalyzer.UI
{
    partial class CalculationReportForm
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
            this.tabControl = new System.Windows.Forms.TabControl();
            this.issuesTab = new System.Windows.Forms.TabPage();
            this.issuesTable = new System.Windows.Forms.TableLayoutPanel();
            this.changesTab = new System.Windows.Forms.TabPage();
            this.changesTable = new System.Windows.Forms.TableLayoutPanel();
            this.unusedTab = new System.Windows.Forms.TabPage();
            this.unusedTable = new System.Windows.Forms.TableLayoutPanel();
            this.tabControl.SuspendLayout();
            this.issuesTab.SuspendLayout();
            this.changesTab.SuspendLayout();
            this.unusedTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.issuesTab);
            this.tabControl.Controls.Add(this.changesTab);
            this.tabControl.Controls.Add(this.unusedTab);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(800, 450);
            this.tabControl.TabIndex = 0;
            // 
            // issuesTab
            // 
            this.issuesTab.Controls.Add(this.issuesTable);
            this.issuesTab.Location = new System.Drawing.Point(4, 22);
            this.issuesTab.Name = "issuesTab";
            this.issuesTab.Padding = new System.Windows.Forms.Padding(3);
            this.issuesTab.Size = new System.Drawing.Size(792, 424);
            this.issuesTab.TabIndex = 0;
            this.issuesTab.Text = "Проблемы";
            this.issuesTab.UseVisualStyleBackColor = true;
            // 
            // issuesTable
            // 
            this.issuesTable.AutoScroll = true;
            this.issuesTable.ColumnCount = 1;
            this.issuesTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.issuesTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.issuesTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.issuesTable.Location = new System.Drawing.Point(3, 3);
            this.issuesTable.Name = "issuesTable";
            this.issuesTable.RowCount = 1;
            this.issuesTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.issuesTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.issuesTable.Size = new System.Drawing.Size(786, 418);
            this.issuesTable.TabIndex = 0;
            // 
            // changesTab
            // 
            this.changesTab.Controls.Add(this.changesTable);
            this.changesTab.Location = new System.Drawing.Point(4, 22);
            this.changesTab.Name = "changesTab";
            this.changesTab.Padding = new System.Windows.Forms.Padding(3);
            this.changesTab.Size = new System.Drawing.Size(792, 424);
            this.changesTab.TabIndex = 1;
            this.changesTab.Text = "Изменения";
            this.changesTab.UseVisualStyleBackColor = true;
            // 
            // changesTable
            // 
            this.changesTable.AutoScroll = true;
            this.changesTable.ColumnCount = 1;
            this.changesTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.changesTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.changesTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.changesTable.Location = new System.Drawing.Point(3, 3);
            this.changesTable.Name = "changesTable";
            this.changesTable.RowCount = 1;
            this.changesTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.changesTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.changesTable.Size = new System.Drawing.Size(786, 418);
            this.changesTable.TabIndex = 0;
            // 
            // unusedTab
            // 
            this.unusedTab.Controls.Add(this.unusedTable);
            this.unusedTab.Location = new System.Drawing.Point(4, 22);
            this.unusedTab.Name = "unusedTab";
            this.unusedTab.Size = new System.Drawing.Size(792, 424);
            this.unusedTab.TabIndex = 2;
            this.unusedTab.Text = "Неиспользуемые";
            this.unusedTab.UseVisualStyleBackColor = true;
            // 
            // unusedTable
            // 
            this.unusedTable.AutoScroll = true;
            this.unusedTable.ColumnCount = 1;
            this.unusedTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.unusedTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.unusedTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.unusedTable.Location = new System.Drawing.Point(0, 0);
            this.unusedTable.Name = "unusedTable";
            this.unusedTable.RowCount = 1;
            this.unusedTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.unusedTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.unusedTable.Size = new System.Drawing.Size(792, 424);
            this.unusedTable.TabIndex = 1;
            // 
            // CalculationReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tabControl);
            this.Name = "CalculationReportForm";
            this.Text = "Отчет";
            this.tabControl.ResumeLayout(false);
            this.issuesTab.ResumeLayout(false);
            this.changesTab.ResumeLayout(false);
            this.unusedTab.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage issuesTab;
        private System.Windows.Forms.TableLayoutPanel issuesTable;
        private System.Windows.Forms.TabPage changesTab;
        private System.Windows.Forms.TableLayoutPanel changesTable;
        private System.Windows.Forms.TabPage unusedTab;
        private System.Windows.Forms.TableLayoutPanel unusedTable;
    }
}