using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace projectCourseOOP
{
    public class OrderExportPDF : IDocument
    {
        private List<Order> orders;

        /// <summary>
        /// Конструктор класса OrderExportPDF
        /// </summary>
        /// <param name="orders"></param>
        public OrderExportPDF(List<Order> orders)
        {
            this.orders = orders;
        }

        /// <summary>
        /// Возвращает метаданные PDF-документа
        /// </summary>
        /// <returns>Метаданные PDF-документа</returns>
        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

        /// <summary>
        /// Основной метод, который формирует содержимое PDF
        /// </summary>
        /// <param name="container"></param>
        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(2, Unit.Centimetre);
                page.PageColor(Colors.White);
                page.DefaultTextStyle(x => x.FontSize(12));

                page.Header()
                    .AlignCenter()
                    .Text("Список заказов")
                    .FontSize(20)
                    .SemiBold();

                page.Content()
                    .PaddingVertical(1, Unit.Centimetre)
                    .Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn();
                            columns.RelativeColumn(5);
                            columns.RelativeColumn(8);
                            columns.RelativeColumn(5);
                            columns.RelativeColumn(3);
                            columns.RelativeColumn(3);
                            columns.RelativeColumn(5);
                        });

                        table.Header(header =>
                        {
                            header.Cell().Text("ID").Bold();
                            header.Cell().Text("Заказчик").Bold();
                            header.Cell().Text("Деталь").Bold();
                            header.Cell().Text("Дата заказа").Bold();
                            header.Cell().Text("Кол-во").Bold();
                            header.Cell().Text("Цена").Bold();
                            header.Cell().Text("Стоимость").Bold();

                            header.Cell().ColumnSpan(6)
                                .PaddingTop(5).BorderBottom(2)
                                .BorderColor(Colors.Grey.Lighten2);
                        });

                        foreach (Order ord in orders)
                        {
                            table.Cell().Text(ord.ID.ToString());
                            table.Cell().Text(ord.ClientName);
                            table.Cell().Text(ord.Detail);
                            table.Cell().Text(ord.OrderDate.ToString("dd.MM.yyyy"));
                            table.Cell().Text(ord.DetailCount.ToString());
                            table.Cell().Text(ord.DetailPrice.ToString());
                            table.Cell().Text(ord.Cost.ToString());
                        }
                    });

                page.Footer()
                    .AlignCenter()
                    .Text(x =>
                    {
                        x.Span("Страница ");
                        x.CurrentPageNumber();
                    });
            });
        }
    }
}
