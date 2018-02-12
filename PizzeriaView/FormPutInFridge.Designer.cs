namespace PizzeriaView
{
    partial class FormPutInFridge
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
            this.comboBoxFridge = new System.Windows.Forms.ComboBox();
            this.labelFridge = new System.Windows.Forms.Label();
            this.labelCount = new System.Windows.Forms.Label();
            this.comboBoxIngredient = new System.Windows.Forms.ComboBox();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.textBoxCount = new System.Windows.Forms.TextBox();
            this.labelIngredient = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // comboBoxFridge
            // 
            this.comboBoxFridge.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFridge.FormattingEnabled = true;
            this.comboBoxFridge.Location = new System.Drawing.Point(87, 6);
            this.comboBoxFridge.Name = "comboBoxFridge";
            this.comboBoxFridge.Size = new System.Drawing.Size(217, 21);
            this.comboBoxFridge.TabIndex = 9;
            // 
            // labelFridge
            // 
            this.labelFridge.AutoSize = true;
            this.labelFridge.Location = new System.Drawing.Point(12, 9);
            this.labelFridge.Name = "labelFridge";
            this.labelFridge.Size = new System.Drawing.Size(77, 13);
            this.labelFridge.TabIndex = 8;
            this.labelFridge.Text = "Холодильник:";
            // 
            // labelCount
            // 
            this.labelCount.AutoSize = true;
            this.labelCount.Location = new System.Drawing.Point(12, 63);
            this.labelCount.Name = "labelCount";
            this.labelCount.Size = new System.Drawing.Size(69, 13);
            this.labelCount.TabIndex = 12;
            this.labelCount.Text = "Количество:";
            // 
            // comboBoxIngredient
            // 
            this.comboBoxIngredient.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxIngredient.FormattingEnabled = true;
            this.comboBoxIngredient.Location = new System.Drawing.Point(87, 33);
            this.comboBoxIngredient.Name = "comboBoxIngredient";
            this.comboBoxIngredient.Size = new System.Drawing.Size(217, 21);
            this.comboBoxIngredient.TabIndex = 11;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(218, 86);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 15;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(137, 86);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 14;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // textBoxCount
            // 
            this.textBoxCount.Location = new System.Drawing.Point(87, 60);
            this.textBoxCount.Name = "textBoxCount";
            this.textBoxCount.Size = new System.Drawing.Size(217, 20);
            this.textBoxCount.TabIndex = 13;
            // 
            // labelIngredient
            // 
            this.labelIngredient.AutoSize = true;
            this.labelIngredient.Location = new System.Drawing.Point(12, 36);
            this.labelIngredient.Name = "labelIngredient";
            this.labelIngredient.Size = new System.Drawing.Size(70, 13);
            this.labelIngredient.TabIndex = 10;
            this.labelIngredient.Text = "Ингредиент:";
            // 
            // FormPutInFridge
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(314, 113);
            this.Controls.Add(this.comboBoxFridge);
            this.Controls.Add(this.labelFridge);
            this.Controls.Add(this.labelCount);
            this.Controls.Add(this.comboBoxIngredient);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.textBoxCount);
            this.Controls.Add(this.labelIngredient);
            this.Name = "FormPutInFridge";
            this.Text = "Пополнение холодильника";
            this.Load += new System.EventHandler(this.FormPutInFridge_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxFridge;
        private System.Windows.Forms.Label labelFridge;
        private System.Windows.Forms.Label labelCount;
        private System.Windows.Forms.ComboBox comboBoxIngredient;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.TextBox textBoxCount;
        private System.Windows.Forms.Label labelIngredient;
    }
}