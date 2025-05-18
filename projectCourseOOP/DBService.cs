using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.ComponentModel;

namespace projectCourseOOP
{
    public class DBService
    {
        public string dbPath;
        private ApplicationContext context;

        /// <summary>
        /// Конструктор класса DatabaseService
        /// </summary>
        /// <param name="dbPath">Путь к базе данных</param>
        public DBService(string dbPath)
        {
            // Инициализация пути к БД и создание контекста
            this.dbPath = dbPath;
            context = new ApplicationContext(this.dbPath);
        }

        /// <summary>
        /// Получение всех заказов из базы данных
        /// </summary>
        /// <returns>Список всех заказов</returns>
        public List<Order> GetOrders()
        {
            // Создаем новый контекст для обеспечения актуальности данных
            context = new ApplicationContext(this.dbPath);
            // Преобразуем данные в список и возвращаем
            return context.Orders.ToList();
        }

        /// <summary>
        /// Создание новой базы данных
        /// </summary>
        /// <param name="filePath">Путь для создания новой БД</param>
        public void CreateNewDatabase(string filePath)
        {
            // Создаем новый контекст для указанного пути
            var context = new ApplicationContext(filePath);

            // Гарантируем, что база данных создана (если не существовала)
            context.Database.EnsureCreated();
        }

        /// <summary>
        /// Создание нового заказа
        /// </summary>
        /// <param name="clientName">Имя клиента</param>
        /// <param name="detail">Название детали</param>
        /// <param name="orderDate">Дата заказа</param>
        /// <param name="detailCount">Количество деталей</param>
        /// <param name="detailPrice">Цена детали</param>
        public void CreateOrder(string clientName, string detail, DateTime orderDate, int detailCount, int detailPrice)
        {
            // Создаем новый объект заказа
            Order order = new(clientName, detail, orderDate, detailCount, detailPrice);

            // Добавляем заказ в контекст и сохраняем изменения
            context.Orders.Add(order);
            context.SaveChanges();
        }

        /// <summary>
        /// Редактирование существующего заказа
        /// </summary>
        /// <param name="orderId">ID редактируемого заказа</param>
        /// <param name="clientName">Новое имя клиента</param>
        /// <param name="detail">Новая деталь</param>
        /// <param name="detailCount">Новое количество</param>
        /// <param name="detailPrice">Новая цена</param>
        public void RedactOrder(int orderId, string clientName, string detail, int detailCount, int detailPrice)
        {
            // Находим заказ по ID
            Order order = FindOrdertById(orderId);

            // Обновляем свойства заказа
            order.ClientName = clientName;
            order.Detail = detail;
            order.DetailCount = detailCount;
            order.DetailPrice = detailPrice;
            order.Cost = detailPrice * detailPrice; // Пересчитываем стоимость

            // Сохраняем изменения и обновляем объект из БД
            context.SaveChanges();
            context.Entry(order).Reload();
        }

        /// <summary>
        /// Удаление заказа
        /// </summary>
        /// <param name="orderId">ID удаляемого заказа</param>
        public void DeleteDocument(int orderId)
        {
            // Находим заказ по ID
            Order order = context.Orders.Find(orderId);

            // Удаляем заказ и сохраняем изменения
            context.Orders.Remove(order);
            context.SaveChanges();
        }

        /// <summary>
        /// Поиск заказа по идентификатору
        /// </summary>
        /// <param name="orderId">ID заказа</param>
        /// <returns>Найденный заказ или null</returns>
        public Order FindOrdertById(int orderId)
        {
            // Очищаем трекер изменений для избежания конфликтов
            context.ChangeTracker.Clear();
            return context.Orders.Find(orderId);
        }

        /// <summary>
        /// Получение заказов за указанный период
        /// </summary>
        /// <param name="fromDate">Начальная дата периода</param>
        /// <param name="toDate">Конечная дата периода</param>
        /// <returns>Кортеж: (список заказов, количество заказов)</returns>
        public (List<Order>, int) GetOrdersByDate(DateTime fromDate, DateTime toDate)
        {
            // Фильтруем заказы по дате и преобразуем в список
            List<Order> resultList = [.. context.Orders.Where(order => order.OrderDate >= fromDate && order.OrderDate <= toDate)];
            int countOfElems = resultList.Count();
            return (resultList, countOfElems);
        }

        /// <summary>
        /// Получение общего количества заказов в БД
        /// </summary>
        /// <returns>Количество заказов</returns>
        public int GetCountOrders()
        {
            return context.Orders.Count();
        }

        /// <summary>
        /// Поиск заказов по названию детали (без учета регистра)
        /// </summary>
        /// <param name="detail">Искомая деталь</param>
        /// <returns>Кортеж: (список найденных заказов, количество заказов)</returns>
        public (List<Order>, int) SearchOrder(string detail)
        {
            // Получаем все заказы и фильтруем по содержанию искомой строки в названии детали
            List<Order> resultList = context.Orders.ToList()
                .Where(d => d.Detail.Contains(detail, StringComparison.OrdinalIgnoreCase)).ToList();
            return (resultList, resultList.Count());
        }

        /// <summary>
        /// Освобождение ресурсов контекста базы данных
        /// </summary>
        public void Dispose()
        {
            context?.Dispose();
        }
    }
}