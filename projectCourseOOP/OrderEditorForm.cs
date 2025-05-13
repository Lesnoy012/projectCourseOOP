using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace projectCourseOOP
{
    public partial class OrderEditorForm : Form
    {
        private DBService dbService;
        private Order order;
        private int orderId;
        private bool edit = false;

        public OrderEditorForm()
        {
            InitializeComponent();
            edit = false;
        }

        public OrderEditorForm(string currentDbPath)
        {
            InitializeComponent();
            dbService = new DBService(currentDbPath);
            edit = false;
        }

        public OrderEditorForm(int orderId, string currentDbPath)
        {
            InitializeComponent();
            this.orderId = orderId;
            dbService = new DBService(currentDbPath);
            order = dbService.FindOrdertById(orderId);
            edit = true;
        }

        private void CreateOrder_Load(object sender, EventArgs e)
        {
            if (edit)
            {
                textBox_FIO.Text = order.ClientName;
                textBox_detail.Text = order.Detail;
                textBox_count.Text = order.DetailCount.ToString();
                textBox_price.Text = order.DetailPrice.ToString();
            }
            else
            {
                textBox_FIO.Text = "Иванов В.В.";
                textBox_detail.Text = "Бампер";
                textBox_count.Text = "1";
                textBox_price.Text = "9000";
            }
        }

        private void button_editor_Click(object sender, EventArgs e)
        {
            if (edit)
            {
                dbService.RedactOrder(orderId, textBox_FIO.Text, textBox_detail.Text, int.Parse(textBox_count.Text), int.Parse(textBox_price.Text));
                this.DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                DateTime dateTime = DateTime.Now;
                string FIO = textBox_FIO.Text;
                string detail = textBox_detail.Text;
                int count = int.Parse(textBox_count.Text);
                int price = int.Parse(textBox_price.Text);

                dbService.CreateOrder(FIO, detail, dateTime, count, price);
                this.DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
