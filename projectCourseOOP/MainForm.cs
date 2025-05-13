using System.Data;
using System.Windows.Forms;
using QuestPDF;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace projectCourseOOP
{
    public partial class MainForm : Form
    {
        private static DBService _dbService;
        private string _currentDb;
        private const string DbFileFilter = "SQLite Database Files (*.db)|*.db";
        private const string PdfFileFilter = "PDF files (*.pdf)|*.pdf";

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            OpenDB(Properties.Settings.Default.LastDb);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_dbService != null)
            {
                Properties.Settings.Default.LastDb = _dbService.dbPath;S
                Properties.Settings.Default.Save();
            }
        }

        #region Button Click Handlers
        private void button_addOrder_Click(object sender, EventArgs e)
        {
            ShowOrderEditor();
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
            if (dataGridView_orders.SelectedRows.Count == 0)
            {
                ShowErrorMessage("Заказ не выбран.");
                return;
            }

            int orderId = (int)dataGridView_orders.SelectedRows[0].Cells["Id"].Value;
            ShowOrderEditor(orderId);
        }

        private void button_deleteOrder_Click(object sender, EventArgs e)
        {
            if (dataGridView_orders.SelectedRows.Count == 0)
            {
                ShowErrorMessage("Заказ не выбран.");
                return;
            }

            int orderId = (int)dataGridView_orders.SelectedRows[0].Cells["Id"].Value;
            _dbService.DeleteDocument(orderId);
            LoadGridView();
        }

        private void button_filter_Click(object sender, EventArgs e)
        {
            using (var formFilter = new FormFilter())
            {
                if (formFilter.ShowDialog() == DialogResult.OK)
                {
                    var (orders, _) = _dbService.GetOrdersByDate(formFilter.fromDate, formFilter.toDate);
                    dataGridView_orders.DataSource = ConvertToDataTable(orders);
                }
            }
        }

        private void button_reset_Click(object sender, EventArgs e)
        {
            LoadGridView();
        }

        private void button_searchDetail_Click(object sender, EventArgs e)
        {
            string detail = textBox_searchDetail.Text;
            var (orders, _) = _dbService.SearchOrder(detail);
            dataGridView_orders.DataSource = ConvertToDataTable(orders);
        }

        private void button_exportPDF_Click(object sender, EventArgs e)
        {
            QuestPDF.Settings.License = LicenseType.Community;
            using (var saveDialog = new SaveFileDialog())
            {
                saveDialog.Filter = PdfFileFilter;
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
        #endregion

        #region Database Operations
        private void CreateDB()
        {
            using (var saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = DbFileFilter;
                saveFileDialog.Title = "Создать новую базу данных";

                if (saveFileDialog.ShowDialog() != DialogResult.OK) return;

                try
                {
                    _currentDb = saveFileDialog.FileName;
                    Text = _currentDb;
                    _dbService = new DBService(_currentDb);
                    _dbService.CreateNewDatabase(_currentDb);
                    dataGridView_orders.DataSource = null;
                    LoadGridView();
                }
                catch (Exception ex)
                {
                    ShowErrorMessage($"Ошибка при создании базы данных: {ex.Message}");
                    Close();
                }
            }
        }

        private void OpenDB(string dbPath = null)
        {
            if (string.IsNullOrEmpty(dbPath))
            {
                using (var openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Filter = DbFileFilter;
                    openFileDialog.Title = "Выбор базы данных";

                    if (openFileDialog.ShowDialog() != DialogResult.OK) return;
                    dbPath = openFileDialog.FileName;
                }
            }

            try
            {
                _dbService = new DBService(dbPath);
                _currentDb = dbPath;
                Text = _currentDb;
                LoadGridView();
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Ошибка при загрузке базы данных: {ex.Message}");
                if (string.IsNullOrEmpty(dbPath)) CreateDB();
            }
        }
        #endregion

        #region Helper Methods
        private void ShowOrderEditor(int? orderId = null)
        {
            using (var orderEditorForm = orderId.HasValue
                ? new OrderEditorForm(orderId.Value, _currentDb)
                : new OrderEditorForm(_currentDb))
            {
                if (orderEditorForm.ShowDialog() == DialogResult.OK)
                {
                    LoadGridView();
                }
            }
        }

        private void LoadGridView()
        {
            var orders = _dbService.GetOrders();
            dataGridView_orders.DataSource = ConvertToDataTable(orders);
        }

        private DataTable ConvertToDataTable(List<Order> orders)
        {
            var table = new DataTable();

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
                table.Rows.Add(
                    order.ID,
                    order.ClientName,
                    order.Detail,
                    order.OrderDate,
                    order.DetailCount,
                    order.DetailPrice,
                    order.Cost
                );
            }

            return table;
        }

        private void ExportToPdf(string filePath)
        {
            try
            {
                var orders = _dbService.GetOrders();
                var pdf = new OrderExportPDF(orders);
                pdf.GeneratePdf(filePath);

                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = filePath,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Ошибка генерации PDF: {ex.Message}");
            }
        }

        private void ShowErrorMessage(string message)
        {
            MessageBox.Show(message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        #endregion
    }
}