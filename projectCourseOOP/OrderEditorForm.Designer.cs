namespace projectCourseOOP
{
    partial class OrderEditorForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OrderEditorForm));
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            textBox_FIO = new TextBox();
            textBox_detail = new TextBox();
            textBox_price = new TextBox();
            textBox_count = new TextBox();
            button_editor = new Button();
            button_cancel = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            label1.Location = new Point(39, 34);
            label1.Name = "label1";
            label1.Size = new Size(137, 21);
            label1.TabIndex = 0;
            label1.Text = "ФИО заказчика:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            label2.Location = new Point(39, 74);
            label2.Name = "label2";
            label2.Size = new Size(85, 21);
            label2.TabIndex = 1;
            label2.Text = "Запчасть:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            label3.Location = new Point(39, 154);
            label3.Name = "label3";
            label3.Size = new Size(55, 21);
            label3.TabIndex = 2;
            label3.Text = "Цена:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            label4.Location = new Point(39, 114);
            label4.Name = "label4";
            label4.Size = new Size(107, 21);
            label4.TabIndex = 3;
            label4.Text = "Количество:";
            // 
            // textBox_FIO
            // 
            textBox_FIO.Location = new Point(182, 32);
            textBox_FIO.Name = "textBox_FIO";
            textBox_FIO.Size = new Size(149, 23);
            textBox_FIO.TabIndex = 4;
            // 
            // textBox_detail
            // 
            textBox_detail.Location = new Point(182, 72);
            textBox_detail.Name = "textBox_detail";
            textBox_detail.Size = new Size(149, 23);
            textBox_detail.TabIndex = 5;
            // 
            // textBox_price
            // 
            textBox_price.Location = new Point(182, 152);
            textBox_price.Name = "textBox_price";
            textBox_price.Size = new Size(149, 23);
            textBox_price.TabIndex = 6;
            // 
            // textBox_count
            // 
            textBox_count.Location = new Point(182, 112);
            textBox_count.Name = "textBox_count";
            textBox_count.Size = new Size(149, 23);
            textBox_count.TabIndex = 7;
            // 
            // button_editor
            // 
            button_editor.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            button_editor.Location = new Point(12, 219);
            button_editor.Name = "button_editor";
            button_editor.Size = new Size(104, 31);
            button_editor.TabIndex = 8;
            button_editor.Text = "Применить";
            button_editor.UseVisualStyleBackColor = true;
            button_editor.Click += button_editor_Click;
            // 
            // button_cancel
            // 
            button_cancel.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            button_cancel.Location = new Point(263, 219);
            button_cancel.Name = "button_cancel";
            button_cancel.Size = new Size(104, 31);
            button_cancel.TabIndex = 9;
            button_cancel.Text = "Отменить";
            button_cancel.UseVisualStyleBackColor = true;
            button_cancel.Click += button_cancel_Click;
            // 
            // OrderEditorForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(379, 262);
            Controls.Add(button_cancel);
            Controls.Add(button_editor);
            Controls.Add(textBox_count);
            Controls.Add(textBox_price);
            Controls.Add(textBox_detail);
            Controls.Add(textBox_FIO);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "OrderEditorForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Создание заказа";
            Load += CreateOrder_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private TextBox textBox_FIO;
        private TextBox textBox_detail;
        private TextBox textBox_price;
        private TextBox textBox_count;
        private Button button_editor;
        private Button button_cancel;
    }
}