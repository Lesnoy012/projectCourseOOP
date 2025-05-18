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
            // При загрузке формы открываем последнюю использованную БД
            OpenDB(Properties.Settings.Default.LastDb);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // При закрытии формы сохраняем путь к текущей БД в настройках
            if (_dbService != null)
            {
                Properties.Settings.Default.LastDb = _dbService.dbPath;
                Properties.Settings.Default.Save();
            }
        }

        #region Button Click Handlers
        // Регион с обработчиками нажатий кнопок

        // Обработчик кнопки добавления нового заказа
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
            // Проверяем, выбран ли заказ в таблице
            if (dataGridView_orders.SelectedRows.Count == 0)
            {
                ShowErrorMessage("Заказ не выбран.");
                return;
            }

            // Получаем ID выбранного заказа и открываем редактор
            int orderId = (int)dataGridView_orders.SelectedRows[0].Cells["Id"].Value;
            ShowOrderEditor(orderId);
        }

        private void button_deleteOrder_Click(object sender, EventArgs e)
        {
            // Проверяем, выбран ли заказ в таблице
            if (dataGridView_orders.SelectedRows.Count == 0)
            {
                ShowErrorMessage("Заказ не выбран.");
                return;
            }

            // Получаем ID выбранного заказа и удаляем его
            int orderId = (int)dataGridView_orders.SelectedRows[0].Cells["Id"].Value;
            _dbService.DeleteDocument(orderId);
            // Обновляем таблицу с заказами
            LoadGridView();
        }

        private void button_filter_Click(object sender, EventArgs e)
        {
            // Открываем форму фильтрации
            using (var formFilter = new FormFilter())
            {
                if (formFilter.ShowDialog() == DialogResult.OK)
                {
                    // Получаем отфильтрованные заказы и обновляем таблицу
                    (List<Order>, int) pair = _dbService.GetOrdersByDate(formFilter.fromDate, formFilter.toDate);
                    dataGridView_orders.DataSource = pair.Item1;
                    labelCountOfElements.Text = pair.Item2 + " из " + _dbService.GetCountOrders();
                }
            }
        }

        private void button_reset_Click(object sender, EventArgs e)
        {
            LoadGridView();
        }

        private void button_searchDetail_Click(object sender, EventArgs e)
        {
            // Получаем текст для поиска из текстового поля
            string detail = textBox_searchDetail.Text;
            // Ищем заказы по детали
            (List<Order>, int) pair = _dbService.SearchOrder(detail);
            List<Order> orders = pair.Item1;
            int countOfElements = pair.Item2;

            // Обновляем таблицу с результатами поиска
            dataGridView_orders.DataSource = ConvertToDataTable(orders);
            labelCountOfElements.Text = countOfElements + " из " + _dbService.GetCountOrders();
        }

        private void button_exportPDF_Click(object sender, EventArgs e)
        {
            // Устанавливаем лицензию для QuestPDF
            QuestPDF.Settings.License = LicenseType.Community;
            using (var saveDialog = new SaveFileDialog())
            {
                saveDialog.Filter = PdfFileFilter;
                // Генерируем имя файла с текущей датой
                saveDialog.FileName = $"Отчёт_по_базе_данных_{DateTime.Now:yyyyMMddhh}.pdf";

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    // Экспортируем данные в PDF
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
        // Регион с операциями работы с БД

        // Метод создания новой БД
        private void CreateDB()
        {
            using (var saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = DbFileFilter;
                saveFileDialog.Title = "Создать новую базу данных";

                if (saveFileDialog.ShowDialog() != DialogResult.OK) return;

                try
                {
                    // Создаем новую БД по выбранному пути
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
            // Если путь не передан, показываем диалог выбора файла
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
                // Открываем БД по указанному пути
                _dbService = new DBService(dbPath);
                _currentDb = dbPath;
                Text = _currentDb;
                LoadGridView();
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Ошибка при загрузке базы данных: {ex.Message}");
                // Если БД не удалось открыть, предлагаем создать новую
                if (string.IsNullOrEmpty(dbPath)) CreateDB();
            }
        }
        #endregion

        #region Helper Methods
        // Регион с вспомогательными методами

        // Метод показа редактора заказов
        private void ShowOrderEditor(int? orderId = null)
        {
            // Создаем форму редактора в зависимости от того, редактируем мы или создаем новый заказ
            using (var orderEditorForm = orderId.HasValue
                ? new OrderEditorForm(orderId.Value, _currentDb)
                : new OrderEditorForm(_currentDb))
            {
                // Если редактирование прошло успешно, обновляем таблицу
                if (orderEditorForm.ShowDialog() == DialogResult.OK)
                {
                    LoadGridView();
                }
            }
        }

        private void LoadGridView()
        {
            // Получаем все заказы из БД и отображаем их в таблице
            var orders = _dbService.GetOrders();
            dataGridView_orders.DataSource = ConvertToDataTable(orders);
            labelCountOfElements.Text = $"{orders.Count} из {_dbService.GetCountOrders()}";
        }

        private DataTable ConvertToDataTable(List<Order> orders)
        {
            var table = new DataTable();

            // Создаем колонки таблицы
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

            // Заполняем таблицу данными из списка заказов
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
                // Получаем все заказы и создаем PDF-документ
                var orders = _dbService.GetOrders();
                var pdf = new OrderExportPDF(orders);
                pdf.GeneratePdf(filePath);

                // Открываем созданный PDF-файл
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