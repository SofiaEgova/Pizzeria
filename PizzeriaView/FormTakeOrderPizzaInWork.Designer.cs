namespace PizzeriaView
{
    partial class FormTakeOrderPizzaInWork
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
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.comboBoxCook = new System.Windows.Forms.ComboBox();
            this.labelCook = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(203, 42);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 13;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(122, 42);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 12;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // comboBoxCook
            // 
            this.comboBoxCook.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCook.FormattingEnabled = true;
            this.comboBoxCook.Location = new System.Drawing.Point(71, 6);
            this.comboBoxCook.Name = "comboBoxCook";
            this.comboBoxCook.Size = new System.Drawing.Size(217, 21);
            this.comboBoxCook.TabIndex = 11;
            // 
            // labelCook
            // 
            this.labelCook.AutoSize = true;
            this.labelCook.Location = new System.Drawing.Point(12, 9);
            this.labelCook.Name = "labelCook";
            this.labelCook.Size = new System.Drawing.Size(42, 13);
            this.labelCook.TabIndex = 10;
            this.labelCook.Text = "Повар:";
            // 
            // FormTakeOrderPizzaInWork
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(325, 70);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.comboBoxCook);
            this.Controls.Add(this.labelCook);
            this.Name = "FormTakeOrderPizzaInWork";
            this.Text = "Отдать заказ в работу";
            this.Load += new System.EventHandler(this.FormTakeOrderPizzaInWork_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.ComboBox comboBoxCook;
        private System.Windows.Forms.Label labelCook;
    }
}