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
            this.dbPath = dbPath;
            context = new ApplicationContext(this.dbPath);
        }

        /// <summary>
        /// Получение таблицы базы данных в виде списка
        /// </summary>
        /// <returns>Таблица базы данных в виде списка</returns>
        public List<Order> GetOrders()
        {
            context = new ApplicationContext(this.dbPath);
            return context.Orders.ToList();
        }

        /// <summary>
        /// Создание новой базы данных
        /// </summary>
        /// <param name="filePath">Путь создания новой базы данных</param>
        public void CreateNewDatabase(string filePath)
        {
            var context = new ApplicationContext(filePath);

            context.Database.EnsureCreated();
        }

        /// <summary>
        /// Создание закааза
        /// </summary>
        /// <param name="ClientName">Имя заказчика</param>
        /// <param name="Detail">Запчасть</param>
        /// <param name="OrderDate">Дата заказа</param>
        /// <param name="DetailCount">Количество запчастей</param>
        /// <param name="DetailPrice">Цена запчасти</param>
        public void CreateOrder(string clientName, string detail, DateTime orderDate, int detailCount, int detailPrice)
        {
            Random rnd = new();
            Order order = new(clientName, detail, orderDate, detailCount, detailPrice);
            context.Orders.Add(order);
            context.SaveChanges();
        }

        /// <summary>
        /// Редактирование заказа
        /// </summary>
        /// <param name="orderId">ID заказа</param>
        /// <param name="clientName">Имя заказчика</param>
        /// <param name="detail">Запчасть</param>
        /// <param name="detailCount">Количество запчастей</param>
        /// <param name="detailPrice">Цена запчасти</param>
        public void RedactOrder(int orderId, string clientName, string detail, int detailCount, int detailPrice)
        {
            Order order = FindOrdertById(orderId);
            order.ClientName = clientName;
            order.Detail = detail;
            order.DetailCount = detailCount;
            order.DetailPrice = detailPrice;
            order.Cost = detailPrice * detailCount;

            context.SaveChanges();
            context.Entry(order).Reload();
        }

        /// <summary>
        /// Удаление заказа
        /// </summary>
        /// <param name="orderId">ID заказа</param>
        public void DeleteDocument(int orderId)
        {
            Order order = context.Orders.Find(orderId);
            context.Orders.Remove(order);
            context.SaveChanges();
        }

        /// <summary>
        /// Поиск заказа по ID
        /// </summary>
        /// <param name="orderId">ID заказа</param>
        /// <returns></returns>
        public Order FindOrdertById(int orderId)
        {
            context.ChangeTracker.Clear();
            return context.Orders.Find(orderId);
        }

        /// <summary>
        /// Получить заказ по дате
        /// </summary>
        /// <param name="fromDate">От какой даты</param>
        /// <param name="toDate">До какой даты</param>
        /// <returns>Список заказов, количество элементов в списке</returns>
        public (List<Order>, int) GetOrdersByDate(DateTime fromDate, DateTime toDate)
        {
            List<Order> resultList = [.. context.Orders.Where(order => order.OrderDate >= fromDate && order.OrderDate <= toDate)];
            int countOfElems = resultList.Count();
            return (resultList, countOfElems);
        }

        /// <summary>
        /// Поиск заказа по детали
        /// </summary>
        /// <param name="detail">заголовок</param>
        /// <returns>Список документов, количество документов в списке</returns>
        public (List<Order>, int) SearchOrder(string detail)
        {
            List<Order> resultList = context.Orders.ToList()
                .Where(d => d.Detail.Contains(detail, StringComparison.OrdinalIgnoreCase)).ToList();
            return (resultList, resultList.Count());
        }

        /// <summary>
        /// Освобождение ресурсов 
        /// </summary>
        public void Dispose()
        {
            context?.Dispose();
        }
    }
}
