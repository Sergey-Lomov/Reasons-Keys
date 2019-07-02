namespace ModelAnalyzer.UI
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.MainLayout = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.actionsPanel = new System.Windows.Forms.Panel();
            this.loadBtn = new System.Windows.Forms.Button();
            this.saveBtn = new System.Windows.Forms.Button();
            this.calculateBtn = new System.Windows.Forms.Button();
            this.filtersGroup = new System.Windows.Forms.GroupBox();
            this.indicatorCB = new System.Windows.Forms.CheckBox();
            this.innerCB = new System.Windows.Forms.CheckBox();
            this.outCB = new System.Windows.Forms.CheckBox();
            this.inCB = new System.Windows.Forms.CheckBox();
            this.bottomPlaceholder = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.titleFilterTB = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.actionsPanel.SuspendLayout();
            this.filtersGroup.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainLayout
            // 
            this.MainLayout.AutoScroll = true;
            this.MainLayout.AutoSize = true;
            this.MainLayout.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.MainLayout.BackColor = System.Drawing.SystemColors.Control;
            this.MainLayout.ColumnCount = 1;
            this.MainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.MainLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainLayout.Location = new System.Drawing.Point(200, 37);
            this.MainLayout.Margin = new System.Windows.Forms.Padding(10);
            this.MainLayout.Name = "MainLayout";
            this.MainLayout.Padding = new System.Windows.Forms.Padding(5);
            this.MainLayout.RowCount = 1;
            this.MainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 525F));
            this.MainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 525F));
            this.MainLayout.Size = new System.Drawing.Size(518, 535);
            this.MainLayout.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.SystemColors.Control;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.actionsPanel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.filtersGroup, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(200, 582);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // actionsPanel
            // 
            this.actionsPanel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.actionsPanel.Controls.Add(this.loadBtn);
            this.actionsPanel.Controls.Add(this.saveBtn);
            this.actionsPanel.Controls.Add(this.calculateBtn);
            this.actionsPanel.Location = new System.Drawing.Point(3, 87);
            this.actionsPanel.MinimumSize = new System.Drawing.Size(194, 117);
            this.actionsPanel.Name = "actionsPanel";
            this.actionsPanel.Size = new System.Drawing.Size(194, 117);
            this.actionsPanel.TabIndex = 1;
            // 
            // loadBtn
            // 
            this.loadBtn.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.loadBtn.AutoSize = true;
            this.loadBtn.Location = new System.Drawing.Point(9, 46);
            this.loadBtn.Name = "loadBtn";
            this.loadBtn.Size = new System.Drawing.Size(175, 23);
            this.loadBtn.TabIndex = 2;
            this.loadBtn.Text = "Загрузить";
            this.loadBtn.UseVisualStyleBackColor = true;
            this.loadBtn.Click += new System.EventHandler(this.LoadFromFile);
            // 
            // saveBtn
            // 
            this.saveBtn.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.saveBtn.AutoSize = true;
            this.saveBtn.Location = new System.Drawing.Point(9, 3);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(175, 23);
            this.saveBtn.TabIndex = 1;
            this.saveBtn.Text = "Сохранить";
            this.saveBtn.UseVisualStyleBackColor = true;
            this.saveBtn.Click += new System.EventHandler(this.SaveToFile);
            // 
            // calculateBtn
            // 
            this.calculateBtn.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.calculateBtn.Location = new System.Drawing.Point(9, 91);
            this.calculateBtn.Name = "calculateBtn";
            this.calculateBtn.Size = new System.Drawing.Size(175, 23);
            this.calculateBtn.TabIndex = 0;
            this.calculateBtn.Text = "Анализ";
            this.calculateBtn.UseVisualStyleBackColor = true;
            this.calculateBtn.Click += new System.EventHandler(this.Calculate);
            // 
            // filtersGroup
            // 
            this.filtersGroup.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.filtersGroup.Controls.Add(this.indicatorCB);
            this.filtersGroup.Controls.Add(this.innerCB);
            this.filtersGroup.Controls.Add(this.outCB);
            this.filtersGroup.Controls.Add(this.inCB);
            this.filtersGroup.Location = new System.Drawing.Point(3, 379);
            this.filtersGroup.MinimumSize = new System.Drawing.Size(194, 115);
            this.filtersGroup.Name = "filtersGroup";
            this.filtersGroup.Size = new System.Drawing.Size(194, 115);
            this.filtersGroup.TabIndex = 2;
            this.filtersGroup.TabStop = false;
            this.filtersGroup.Text = "Фильтр";
            // 
            // indicatorCB
            // 
            this.indicatorCB.AutoSize = true;
            this.indicatorCB.Checked = true;
            this.indicatorCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.indicatorCB.Location = new System.Drawing.Point(7, 92);
            this.indicatorCB.Name = "indicatorCB";
            this.indicatorCB.Size = new System.Drawing.Size(89, 17);
            this.indicatorCB.TabIndex = 3;
            this.indicatorCB.Text = "Индикаторы";
            this.indicatorCB.UseVisualStyleBackColor = true;
            this.indicatorCB.CheckedChanged += new System.EventHandler(this.FilterChanged);
            // 
            // innerCB
            // 
            this.innerCB.AutoSize = true;
            this.innerCB.Location = new System.Drawing.Point(7, 68);
            this.innerCB.Name = "innerCB";
            this.innerCB.Size = new System.Drawing.Size(85, 17);
            this.innerCB.TabIndex = 2;
            this.innerCB.Text = "Внутренние";
            this.innerCB.UseVisualStyleBackColor = true;
            this.innerCB.CheckedChanged += new System.EventHandler(this.FilterChanged);
            // 
            // outCB
            // 
            this.outCB.AutoSize = true;
            this.outCB.Checked = true;
            this.outCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.outCB.Location = new System.Drawing.Point(7, 44);
            this.outCB.Name = "outCB";
            this.outCB.Size = new System.Drawing.Size(84, 17);
            this.outCB.TabIndex = 1;
            this.outCB.Text = "Исходящие";
            this.outCB.UseVisualStyleBackColor = true;
            this.outCB.CheckedChanged += new System.EventHandler(this.FilterChanged);
            // 
            // inCB
            // 
            this.inCB.AutoSize = true;
            this.inCB.Checked = true;
            this.inCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.inCB.Location = new System.Drawing.Point(7, 20);
            this.inCB.Name = "inCB";
            this.inCB.Size = new System.Drawing.Size(77, 17);
            this.inCB.TabIndex = 0;
            this.inCB.Text = "Входящие";
            this.inCB.UseVisualStyleBackColor = true;
            this.inCB.CheckedChanged += new System.EventHandler(this.FilterChanged);
            // 
            // bottomPlaceholder
            // 
            this.bottomPlaceholder.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottomPlaceholder.Location = new System.Drawing.Point(200, 572);
            this.bottomPlaceholder.Name = "bottomPlaceholder";
            this.bottomPlaceholder.Size = new System.Drawing.Size(518, 10);
            this.bottomPlaceholder.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.titleFilterTB);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(200, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(518, 37);
            this.panel1.TabIndex = 0;
            // 
            // titleFilterTB
            // 
            this.titleFilterTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.titleFilterTB.Location = new System.Drawing.Point(8, 12);
            this.titleFilterTB.Name = "titleFilterTB";
            this.titleFilterTB.Size = new System.Drawing.Size(485, 20);
            this.titleFilterTB.TabIndex = 0;
            this.titleFilterTB.TextChanged += new System.EventHandler(this.FilterChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(718, 582);
            this.Controls.Add(this.MainLayout);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.bottomPlaceholder);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MinimumSize = new System.Drawing.Size(600, 400);
            this.Name = "MainForm";
            this.Text = "ModelManager";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.actionsPanel.ResumeLayout(false);
            this.actionsPanel.PerformLayout();
            this.filtersGroup.ResumeLayout(false);
            this.filtersGroup.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel MainLayout;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel actionsPanel;
        private System.Windows.Forms.Button calculateBtn;
        private System.Windows.Forms.Button loadBtn;
        private System.Windows.Forms.Button saveBtn;
        private System.Windows.Forms.GroupBox filtersGroup;
        private System.Windows.Forms.CheckBox indicatorCB;
        private System.Windows.Forms.CheckBox innerCB;
        private System.Windows.Forms.CheckBox outCB;
        private System.Windows.Forms.CheckBox inCB;
        private System.Windows.Forms.Panel bottomPlaceholder;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox titleFilterTB;
    }
}

