using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace projectCourseOOP
{
    public partial class FormFilter : Form
    {
        public DateTime fromDate;
        public DateTime toDate;

        public FormFilter()
        {
            InitializeComponent();
        }

        private void button_apply_Click(object sender, EventArgs e)
        {
            fromDate = dateTimePickerFrom.Value;
            toDate = dateTimePickerTo.Value;

            if (fromDate > toDate)
            {
                MessageBox.Show("Неправильно выбрана дата!");
                return;
            }
            this.DialogResult = DialogResult.OK;
            Close();
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            Close();
        }
    }
}
