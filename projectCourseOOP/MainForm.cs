using System.Data;
using System.Reflection.Metadata;
using System.Windows.Forms;
using QuestPDF;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace projectCourseOOP
{
    public partial class MainForm : Form
    {
        private static DBService DBService { get; set; } = null!;
        private string CurrentDB { get; set; }

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            OpenDB(Properties.Settings.Default.LastDb);
        }

        private void button_addOrder_Click(object sender, EventArgs e)
        {
            OrderEditorForm createOrderForm = new(CurrentDB);
            DialogResult result = createOrderForm.ShowDialog();
            if (result == DialogResult.OK)
            {
                LoadGridView();
            }
        }

        private void button_openDB_Click(object sender, EventArgs e)
        {
            OpenDB();
        }

        private void button_createDB_Click(object sender, EventArgs e)
        {
            CreateDB();
        }

        private void button_changeOrder_Click(object sender, EventArgs e)
        {
            if (dataGridView_orders.SelectedRows.Count > 0)
            {
                int orderId = (int)dataGridView_orders.SelectedRows[0].Cells["Id"].Value;

                OrderEditorForm orderEditorForm = new(orderId, CurrentDB);
                DialogResult result = orderEditorForm.ShowDialog();
                if (result == DialogResult.OK)
                {
                    LoadGridView();
                }
            }
            else
            {
                MessageBox.Show("Заказ не выбран.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CreateDB()
        {
            SaveFileDialog saveFileDialog = new()
            {
                Filter = "SQLite Database Files (*.db)|*.db",
                Title = "Создать новую базу данных"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    CurrentDB = saveFileDialog.FileName;
                    this.Text = CurrentDB;
                    DBService = new(CurrentDB);
                    DBService.CreateNewDatabase(CurrentDB);
                    dataGridView_orders.DataSource = null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при создании базы данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                }

                LoadGridView();
            }
        }

        private void LoadGridView()
        {
            List<Order> orders = DBService.GetOrders();
            dataGridView_orders.DataSource = ConvertToDataTable(orders);
        }

        private void OpenDB()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "SQLite Database Files (*.db)|*.db";
            openFileDialog.Title = "Выбор базы данных";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string selectedDbPath = openFileDialog.FileName;

                    DBService = new DBService(selectedDbPath);
                    CurrentDB = selectedDbPath;
                    this.Text = CurrentDB;

                    LoadGridView();

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при загрузке базы данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void OpenDB(string lastBD)
        {
            try
            {
                DBService = new DBService(lastBD);
                CurrentDB = lastBD;
                this.Text = CurrentDB;

                LoadGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке базы данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CreateDB();
            }
        }

        private DataTable ConvertToDataTable(List<Order> orders)
        {
            DataTable table = new DataTable();

            table.Columns.AddRange(new[]
            {
                new DataColumn("ID", typeof(int)),
                new DataColumn("Заказчик", typeof(string)),
                new DataColumn("Запчасть", typeof(string)),
                new DataColumn("Дата создания", typeof(DateTime)),
                new DataColumn("Количество", typeof(string)),
                new DataColumn("Цена", typeof(string)),
                new DataColumn("Стоимость", typeof(string))
            });

            foreach (var order in orders)
            {
                table.Rows.Add
                    (
                    order.ID, order.ClientName, order.Detail,
                    order.OrderDate, order.DetailCount,
                    order.DetailPrice, order.Cost
                    );
            }

            return table;
        }

        private void MainForm_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.LastDb = DBService.dbPath;
            Properties.Settings.Default.Save();
        }

        private void button_deleteOrder_Click(object sender, EventArgs e)
        {
            if (dataGridView_orders.SelectedRows.Count > 0)
            {
                int orderId = (int)dataGridView_orders.SelectedRows[0].Cells["Id"].Value;
                DBService.DeleteDocument(orderId);
                LoadGridView();
            }
            else
            {
                MessageBox.Show("Заказ не выбран.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button_filter_Click(object sender, EventArgs e)
        {
            FormFilter formFilter = new FormFilter();
            DialogResult result = formFilter.ShowDialog();

            if (result == DialogResult.OK)
            {
                (List<Order>, int) pair = DBService.GetOrdersByDate(formFilter.fromDate, formFilter.toDate);
                dataGridView_orders.DataSource = pair.Item1;
            }
        }

        private void button_reset_Click(object sender, EventArgs e)
        {
            LoadGridView();
        }

        private void button_searchDetail_Click(object sender, EventArgs e)
        {
            string detail = textBox_searchDetail.Text;
            (List<Order>, int) pair = DBService.SearchOrder(detail);
            List<Order> filteredDocuments = pair.Item1;

            dataGridView_orders.DataSource = ConvertToDataTable(filteredDocuments);
        }

        public void ExportToPdf(string filePath)
        {
            try
            {
                List<Order> orders = DBService.GetOrders();

                OrderExportPDF pdf = new(orders);

                pdf.GeneratePdf(filePath);

                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = filePath,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка генерации PDF: {ex.Message}",
                              "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button_exportPDF_Click(object sender, EventArgs e)
        {
            QuestPDF.Settings.License = LicenseType.Community;
            using (var saveDialog = new SaveFileDialog())
            {
                saveDialog.Filter = "PDF files (*.pdf)|*.pdf";
                saveDialog.FileName = $"Отчёт_по_базе_данных_{DateTime.Now:yyyyMMddhh}.pdf";

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    ExportToPdf(saveDialog.FileName);
                }
            }
        }

        private void button_close_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
