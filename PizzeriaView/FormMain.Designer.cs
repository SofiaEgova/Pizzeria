namespace PizzeriaView
{
    partial class FormMain
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.справочникиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.посетителиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ингредиентыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.пиццыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.холодильникиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.повараToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.пополнитьХолодильникToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.отчетыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.прайсПиццToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.загруженностьХолодильниковToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.заказToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonRef = new System.Windows.Forms.Button();
            this.buttonPayOrder = new System.Windows.Forms.Button();
            this.buttonOrderReady = new System.Windows.Forms.Button();
            this.buttonTakeOrderInWork = new System.Windows.Forms.Button();
            this.buttonOrderPizza = new System.Windows.Forms.Button();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.письмаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.справочникиToolStripMenuItem,
            this.пополнитьХолодильникToolStripMenuItem,
            this.отчетыToolStripMenuItem,
            this.письмаToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(931, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // справочникиToolStripMenuItem
            // 
            this.справочникиToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.посетителиToolStripMenuItem,
            this.ингредиентыToolStripMenuItem,
            this.пиццыToolStripMenuItem,
            this.холодильникиToolStripMenuItem,
            this.повараToolStripMenuItem});
            this.справочникиToolStripMenuItem.Name = "справочникиToolStripMenuItem";
            this.справочникиToolStripMenuItem.Size = new System.Drawing.Size(94, 20);
            this.справочникиToolStripMenuItem.Text = "Справочники";
            // 
            // посетителиToolStripMenuItem
            // 
            this.посетителиToolStripMenuItem.Name = "посетителиToolStripMenuItem";
            this.посетителиToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.посетителиToolStripMenuItem.Text = "Посетители";
            this.посетителиToolStripMenuItem.Click += new System.EventHandler(this.посетителиToolStripMenuItem_Click);
            // 
            // ингредиентыToolStripMenuItem
            // 
            this.ингредиентыToolStripMenuItem.Name = "ингредиентыToolStripMenuItem";
            this.ингредиентыToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.ингредиентыToolStripMenuItem.Text = "Ингредиенты";
            this.ингредиентыToolStripMenuItem.Click += new System.EventHandler(this.ингредиентыToolStripMenuItem_Click);
            // 
            // пиццыToolStripMenuItem
            // 
            this.пиццыToolStripMenuItem.Name = "пиццыToolStripMenuItem";
            this.пиццыToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.пиццыToolStripMenuItem.Text = "Пиццы";
            this.пиццыToolStripMenuItem.Click += new System.EventHandler(this.пиццыToolStripMenuItem_Click);
            // 
            // холодильникиToolStripMenuItem
            // 
            this.холодильникиToolStripMenuItem.Name = "холодильникиToolStripMenuItem";
            this.холодильникиToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.холодильникиToolStripMenuItem.Text = "Холодильники";
            this.холодильникиToolStripMenuItem.Click += new System.EventHandler(this.холодильникиToolStripMenuItem_Click);
            // 
            // повараToolStripMenuItem
            // 
            this.повараToolStripMenuItem.Name = "повараToolStripMenuItem";
            this.повараToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.повараToolStripMenuItem.Text = "Повара";
            this.повараToolStripMenuItem.Click += new System.EventHandler(this.повараToolStripMenuItem_Click);
            // 
            // пополнитьХолодильникToolStripMenuItem
            // 
            this.пополнитьХолодильникToolStripMenuItem.Name = "пополнитьХолодильникToolStripMenuItem";
            this.пополнитьХолодильникToolStripMenuItem.Size = new System.Drawing.Size(156, 20);
            this.пополнитьХолодильникToolStripMenuItem.Text = "Пополнить холодильник";
            this.пополнитьХолодильникToolStripMenuItem.Click += new System.EventHandler(this.пополнитьХолодильникToolStripMenuItem_Click);
            // 
            // отчетыToolStripMenuItem
            // 
            this.отчетыToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.прайсПиццToolStripMenuItem,
            this.загруженностьХолодильниковToolStripMenuItem,
            this.заказToolStripMenuItem});
            this.отчетыToolStripMenuItem.Name = "отчетыToolStripMenuItem";
            this.отчетыToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.отчетыToolStripMenuItem.Text = "Отчеты";
            // 
            // прайсПиццToolStripMenuItem
            // 
            this.прайсПиццToolStripMenuItem.Name = "прайсПиццToolStripMenuItem";
            this.прайсПиццToolStripMenuItem.Size = new System.Drawing.Size(246, 22);
            this.прайсПиццToolStripMenuItem.Text = "Прайс пицц";
            this.прайсПиццToolStripMenuItem.Click += new System.EventHandler(this.прайсПиццToolStripMenuItem_Click);
            // 
            // загруженностьХолодильниковToolStripMenuItem
            // 
            this.загруженностьХолодильниковToolStripMenuItem.Name = "загруженностьХолодильниковToolStripMenuItem";
            this.загруженностьХолодильниковToolStripMenuItem.Size = new System.Drawing.Size(246, 22);
            this.загруженностьХолодильниковToolStripMenuItem.Text = "Загруженность холодильников";
            this.загруженностьХолодильниковToolStripMenuItem.Click += new System.EventHandler(this.загруженностьХолодильниковToolStripMenuItem_Click);
            // 
            // заказToolStripMenuItem
            // 
            this.заказToolStripMenuItem.Name = "заказToolStripMenuItem";
            this.заказToolStripMenuItem.Size = new System.Drawing.Size(246, 22);
            this.заказToolStripMenuItem.Text = "Заказы посетителей";
            this.заказToolStripMenuItem.Click += new System.EventHandler(this.заказToolStripMenuItem_Click);
            // 
            // buttonRef
            // 
            this.buttonRef.Location = new System.Drawing.Point(770, 246);
            this.buttonRef.Name = "buttonRef";
            this.buttonRef.Size = new System.Drawing.Size(149, 23);
            this.buttonRef.TabIndex = 10;
            this.buttonRef.Text = "Обновить список";
            this.buttonRef.UseVisualStyleBackColor = true;
            this.buttonRef.Click += new System.EventHandler(this.buttonRef_Click);
            // 
            // buttonPayOrder
            // 
            this.buttonPayOrder.Location = new System.Drawing.Point(770, 195);
            this.buttonPayOrder.Name = "buttonPayOrder";
            this.buttonPayOrder.Size = new System.Drawing.Size(149, 23);
            this.buttonPayOrder.TabIndex = 9;
            this.buttonPayOrder.Text = "Заказ оплачен";
            this.buttonPayOrder.UseVisualStyleBackColor = true;
            this.buttonPayOrder.Click += new System.EventHandler(this.buttonPayOrder_Click);
            // 
            // buttonOrderReady
            // 
            this.buttonOrderReady.Location = new System.Drawing.Point(770, 143);
            this.buttonOrderReady.Name = "buttonOrderReady";
            this.buttonOrderReady.Size = new System.Drawing.Size(149, 23);
            this.buttonOrderReady.TabIndex = 8;
            this.buttonOrderReady.Text = "Заказ готов";
            this.buttonOrderReady.UseVisualStyleBackColor = true;
            this.buttonOrderReady.Click += new System.EventHandler(this.buttonOrderReady_Click);
            // 
            // buttonTakeOrderInWork
            // 
            this.buttonTakeOrderInWork.Location = new System.Drawing.Point(770, 96);
            this.buttonTakeOrderInWork.Name = "buttonTakeOrderInWork";
            this.buttonTakeOrderInWork.Size = new System.Drawing.Size(149, 23);
            this.buttonTakeOrderInWork.TabIndex = 7;
            this.buttonTakeOrderInWork.Text = "Отдать на выполнение";
            this.buttonTakeOrderInWork.UseVisualStyleBackColor = true;
            this.buttonTakeOrderInWork.Click += new System.EventHandler(this.buttonTakeOrderInWork_Click);
            // 
            // buttonOrderPizza
            // 
            this.buttonOrderPizza.Location = new System.Drawing.Point(770, 45);
            this.buttonOrderPizza.Name = "buttonOrderPizza";
            this.buttonOrderPizza.Size = new System.Drawing.Size(149, 23);
            this.buttonOrderPizza.TabIndex = 6;
            this.buttonOrderPizza.Text = "Заказать пиццу";
            this.buttonOrderPizza.UseVisualStyleBackColor = true;
            this.buttonOrderPizza.Click += new System.EventHandler(this.buttonOrderPizza_Click);
            // 
            // dataGridView
            // 
            this.dataGridView.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(13, 28);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.Size = new System.Drawing.Size(737, 317);
            this.dataGridView.TabIndex = 11;
            // 
            // письмаToolStripMenuItem
            // 
            this.письмаToolStripMenuItem.Name = "письмаToolStripMenuItem";
            this.письмаToolStripMenuItem.Size = new System.Drawing.Size(62, 20);
            this.письмаToolStripMenuItem.Text = "Письма";
            this.письмаToolStripMenuItem.Click += new System.EventHandler(this.письмаToolStripMenuItem_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(931, 373);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.buttonRef);
            this.Controls.Add(this.buttonPayOrder);
            this.Controls.Add(this.buttonOrderReady);
            this.Controls.Add(this.buttonTakeOrderInWork);
            this.Controls.Add(this.buttonOrderPizza);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormMain";
            this.Text = "Пиццерия";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem справочникиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem посетителиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ингредиентыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem пиццыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem холодильникиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem повараToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem пополнитьХолодильникToolStripMenuItem;
        private System.Windows.Forms.Button buttonRef;
        private System.Windows.Forms.Button buttonPayOrder;
        private System.Windows.Forms.Button buttonOrderReady;
        private System.Windows.Forms.Button buttonTakeOrderInWork;
        private System.Windows.Forms.Button buttonOrderPizza;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.ToolStripMenuItem отчетыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem прайсПиццToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem загруженностьХолодильниковToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem заказToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem письмаToolStripMenuItem;
    }
}