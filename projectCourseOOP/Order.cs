using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectCourseOOP
{
    public class Order
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string ClientName { get; set; } = "Отсутствует значение";
        public string Detail { get; set; } = "Отсутствует значение";
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public int DetailCount { get; set; } = 0;
        public int DetailPrice { get; set; } = 0;
        public int Cost { get; set; } = 0;

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public Order() { }

        /// <summary>
        /// Конструктор с параметрами
        /// </summary>
        /// <param name="ClientName">Имя заказчика</param>
        /// <param name="Detail">Запчасть</param>
        /// <param name="OrderDate">Дата заказа</param>
        /// <param name="DetailCount">Количество запчастей</param>
        /// <param name="DetailPrice">Цена запчасти</param>
        public Order(string clientName, string detail, DateTime orderDate, int detailCount, int detailPrice)
        {
            ClientName = clientName;
            Detail = detail;
            OrderDate = orderDate;
            DetailCount = detailCount;
            DetailPrice = detailPrice;
            Cost = detailPrice * detailCount;
        }
    }
}
