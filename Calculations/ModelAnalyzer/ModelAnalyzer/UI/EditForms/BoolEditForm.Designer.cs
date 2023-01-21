
namespace ModelAnalyzer.UI.EditForms
{
    partial class BoolEditForm
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
            this.cancelButton = new System.Windows.Forms.Button();
            this.calculateButton = new System.Windows.Forms.Button();
            this.approveButton = new System.Windows.Forms.Button();
            this.valueCheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(12, 112);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(159, 23);
            this.cancelButton.TabIndex = 7;
            this.cancelButton.Text = "Отменить";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // calculateButton
            // 
            this.calculateButton.Location = new System.Drawing.Point(12, 83);
            this.calculateButton.Name = "calculateButton";
            this.calculateButton.Size = new System.Drawing.Size(159, 23);
            this.calculateButton.TabIndex = 6;
            this.calculateButton.Text = "Обновить всё";
            this.calculateButton.UseVisualStyleBackColor = true;
            this.calculateButton.Click += new System.EventHandler(this.CalculateButton_Click);
            // 
            // approveButton
            // 
            this.approveButton.Location = new System.Drawing.Point(12, 54);
            this.approveButton.Name = "approveButton";
            this.approveButton.Size = new System.Drawing.Size(159, 23);
            this.approveButton.TabIndex = 5;
            this.approveButton.Text = "Обновить";
            this.approveButton.UseVisualStyleBackColor = true;
            this.approveButton.Click += new System.EventHandler(this.ApproveButton_Click);
            // 
            // valueCheckBox
            // 
            this.valueCheckBox.AutoSize = true;
            this.valueCheckBox.Location = new System.Drawing.Point(12, 20);
            this.valueCheckBox.Name = "valueCheckBox";
            this.valueCheckBox.Size = new System.Drawing.Size(119, 17);
            this.valueCheckBox.TabIndex = 8;
            this.valueCheckBox.Text = "Parameter edit lable";
            this.valueCheckBox.UseVisualStyleBackColor = true;
            // 
            // BoolEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(184, 155);
            this.Controls.Add(this.valueCheckBox);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.calculateButton);
            this.Controls.Add(this.approveButton);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BoolEditForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Редактирование";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button calculateButton;
        private System.Windows.Forms.Button approveButton;
        private System.Windows.Forms.CheckBox valueCheckBox;
    }
}