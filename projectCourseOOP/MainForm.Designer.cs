namespace projectCourseOOP
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            dataGridView_orders = new DataGridView();
            button_addOrder = new Button();
            button_openDB = new Button();
            button_createDB = new Button();
            button_changeOrder = new Button();
            button_deleteOrder = new Button();
            button_filter = new Button();
            button_reset = new Button();
            textBox_searchDetail = new TextBox();
            button_searchDetail = new Button();
            button_exportPDF = new Button();
            button1 = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView_orders).BeginInit();
            SuspendLayout();
            // 
            // dataGridView_orders
            // 
            dataGridView_orders.AllowUserToAddRows = false;
            dataGridView_orders.AllowUserToDeleteRows = false;
            dataGridView_orders.AllowUserToResizeColumns = false;
            dataGridView_orders.AllowUserToResizeRows = false;
            dataGridView_orders.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView_orders.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView_orders.Location = new Point(12, 32);
            dataGridView_orders.Name = "dataGridView_orders";
            dataGridView_orders.ReadOnly = true;
            dataGridView_orders.RightToLeft = RightToLeft.No;
            dataGridView_orders.Size = new Size(870, 297);
            dataGridView_orders.TabIndex = 0;
            // 
            // button_addOrder
            // 
            button_addOrder.Cursor = Cursors.Hand;
            button_addOrder.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            button_addOrder.Location = new Point(260, 353);
            button_addOrder.Name = "button_addOrder";
            button_addOrder.Size = new Size(118, 54);
            button_addOrder.TabIndex = 1;
            button_addOrder.Text = "Добавить заказ";
            button_addOrder.UseVisualStyleBackColor = true;
            button_addOrder.Click += button_addOrder_Click;
            // 
            // button_openDB
            // 
            button_openDB.Cursor = Cursors.Hand;
            button_openDB.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            button_openDB.Location = new Point(136, 353);
            button_openDB.Name = "button_openDB";
            button_openDB.Size = new Size(118, 54);
            button_openDB.TabIndex = 2;
            button_openDB.Text = "Открыть БД";
            button_openDB.UseVisualStyleBackColor = true;
            button_openDB.Click += button_openDB_Click;
            // 
            // button_createDB
            // 
            button_createDB.Cursor = Cursors.Hand;
            button_createDB.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            button_createDB.Location = new Point(12, 353);
            button_createDB.Name = "button_createDB";
            button_createDB.Size = new Size(118, 54);
            button_createDB.TabIndex = 3;
            button_createDB.Text = "Создать БД";
            button_createDB.UseVisualStyleBackColor = true;
            button_createDB.Click += button_createDB_Click;
            // 
            // button_changeOrder
            // 
            button_changeOrder.Cursor = Cursors.Hand;
            button_changeOrder.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            button_changeOrder.Location = new Point(508, 353);
            button_changeOrder.Name = "button_changeOrder";
            button_changeOrder.Size = new Size(118, 54);
            button_changeOrder.TabIndex = 4;
            button_changeOrder.Text = "Редактировать";
            button_changeOrder.UseVisualStyleBackColor = true;
            button_changeOrder.Click += button_changeOrder_Click;
            // 
            // button_deleteOrder
            // 
            button_deleteOrder.Cursor = Cursors.Hand;
            button_deleteOrder.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            button_deleteOrder.Location = new Point(384, 353);
            button_deleteOrder.Name = "button_deleteOrder";
            button_deleteOrder.Size = new Size(118, 54);
            button_deleteOrder.TabIndex = 5;
            button_deleteOrder.Text = "Удалить заказ";
            button_deleteOrder.UseVisualStyleBackColor = true;
            button_deleteOrder.Click += button_deleteOrder_Click;
            // 
            // button_filter
            // 
            button_filter.Cursor = Cursors.Hand;
            button_filter.Location = new Point(11, 3);
            button_filter.Name = "button_filter";
            button_filter.Size = new Size(75, 23);
            button_filter.TabIndex = 6;
            button_filter.Text = "Фильтр";
            button_filter.UseVisualStyleBackColor = true;
            button_filter.Click += button_filter_Click;
            // 
            // button_reset
            // 
            button_reset.BackColor = SystemColors.Control;
            button_reset.BackgroundImageLayout = ImageLayout.Center;
            button_reset.Cursor = Cursors.Hand;
            button_reset.ForeColor = SystemColors.ControlText;
            button_reset.Location = new Point(806, 4);
            button_reset.Name = "button_reset";
            button_reset.Size = new Size(74, 22);
            button_reset.TabIndex = 7;
            button_reset.Text = "Сбросить";
            button_reset.UseVisualStyleBackColor = false;
            button_reset.Click += button_reset_Click;
            // 
            // textBox_searchDetail
            // 
            textBox_searchDetail.Location = new Point(206, 3);
            textBox_searchDetail.Name = "textBox_searchDetail";
            textBox_searchDetail.Size = new Size(125, 23);
            textBox_searchDetail.TabIndex = 8;
            // 
            // button_searchDetail
            // 
            button_searchDetail.Cursor = Cursors.Hand;
            button_searchDetail.Location = new Point(92, 3);
            button_searchDetail.Name = "button_searchDetail";
            button_searchDetail.Size = new Size(108, 23);
            button_searchDetail.TabIndex = 9;
            button_searchDetail.Text = "Найти по детали";
            button_searchDetail.UseVisualStyleBackColor = true;
            button_searchDetail.Click += button_searchDetail_Click;
            // 
            // button_exportPDF
            // 
            button_exportPDF.Cursor = Cursors.Hand;
            button_exportPDF.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            button_exportPDF.Location = new Point(632, 353);
            button_exportPDF.Name = "button_exportPDF";
            button_exportPDF.Size = new Size(122, 54);
            button_exportPDF.TabIndex = 10;
            button_exportPDF.Text = "Экспорт PDF";
            button_exportPDF.UseVisualStyleBackColor = true;
            button_exportPDF.Click += button_exportPDF_Click;
            // 
            // button1
            // 
            button1.Cursor = Cursors.Hand;
            button1.Image = (Image)resources.GetObject("button1.Image");
            button1.Location = new Point(762, 353);
            button1.Name = "button1";
            button1.Size = new Size(118, 54);
            button1.TabIndex = 11;
            button1.UseVisualStyleBackColor = true;
            button1.Click += button_close_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(892, 419);
            Controls.Add(button1);
            Controls.Add(button_exportPDF);
            Controls.Add(button_searchDetail);
            Controls.Add(textBox_searchDetail);
            Controls.Add(button_reset);
            Controls.Add(button_filter);
            Controls.Add(button_deleteOrder);
            Controls.Add(button_changeOrder);
            Controls.Add(button_createDB);
            Controls.Add(button_openDB);
            Controls.Add(button_addOrder);
            Controls.Add(dataGridView_orders);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MainForm";
            FormClosing += MainForm_FormClosing_1;
            Load += MainForm_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView_orders).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView_orders;
        private Button button_addOrder;
        private Button button_openDB;
        private Button button_createDB;
        private Button button_changeOrder;
        private Button button_deleteOrder;
        private Button button_filter;
        private Button button_reset;
        private TextBox textBox_searchDetail;
        private Button button_searchDetail;
        private Button button_exportPDF;
        private Button button1;
    }
}
