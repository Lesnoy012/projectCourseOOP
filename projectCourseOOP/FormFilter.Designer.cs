namespace projectCourseOOP
{
    partial class FormFilter
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormFilter));
            label1 = new Label();
            dateTimePickerFrom = new DateTimePicker();
            dateTimePickerTo = new DateTimePicker();
            label2 = new Label();
            label3 = new Label();
            button_apply = new Button();
            button_cancel = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            label1.Location = new Point(75, 17);
            label1.Name = "label1";
            label1.Size = new Size(179, 21);
            label1.TabIndex = 0;
            label1.Text = "Фильтровать по дате:";
            // 
            // dateTimePickerFrom
            // 
            dateTimePickerFrom.Location = new Point(78, 57);
            dateTimePickerFrom.Name = "dateTimePickerFrom";
            dateTimePickerFrom.Size = new Size(200, 23);
            dateTimePickerFrom.TabIndex = 1;
            // 
            // dateTimePickerTo
            // 
            dateTimePickerTo.Location = new Point(78, 93);
            dateTimePickerTo.Name = "dateTimePickerTo";
            dateTimePickerTo.Size = new Size(200, 23);
            dateTimePickerTo.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label2.Location = new Point(41, 59);
            label2.Name = "label2";
            label2.Size = new Size(33, 20);
            label2.TabIndex = 3;
            label2.Text = "От: ";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label3.Location = new Point(39, 94);
            label3.Name = "label3";
            label3.Size = new Size(35, 20);
            label3.TabIndex = 4;
            label3.Text = "До: ";
            // 
            // button_apply
            // 
            button_apply.Location = new Point(12, 134);
            button_apply.Name = "button_apply";
            button_apply.Size = new Size(78, 26);
            button_apply.TabIndex = 5;
            button_apply.Text = "Применить";
            button_apply.UseVisualStyleBackColor = true;
            button_apply.Click += button_apply_Click;
            // 
            // button_cancel
            // 
            button_cancel.Location = new Point(231, 134);
            button_cancel.Name = "button_cancel";
            button_cancel.Size = new Size(78, 26);
            button_cancel.TabIndex = 6;
            button_cancel.Text = "Отменить";
            button_cancel.UseVisualStyleBackColor = true;
            button_cancel.Click += button_cancel_Click;
            // 
            // FormFilter
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(321, 168);
            Controls.Add(button_cancel);
            Controls.Add(button_apply);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(dateTimePickerTo);
            Controls.Add(dateTimePickerFrom);
            Controls.Add(label1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "FormFilter";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Фильтр";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private DateTimePicker dateTimePickerFrom;
        private DateTimePicker dateTimePickerTo;
        private Label label2;
        private Label label3;
        private Button button_apply;
        private Button button_cancel;
    }
}