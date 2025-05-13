using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectCourseOOP
{
    public class ApplicationContext : DbContext
    {
        private readonly string dbPath;

        /// <summary>
        /// Коллекция, представляющая таблицу базы данных
        /// </summary>
        public DbSet<Order> Orders { get; set; } = null!;

        /// <summary>
        /// Конструктор класса ApplicationContext
        /// </summary>
        /// <param name="dbPath"></param>
        public ApplicationContext(string dbPath)
        {
            this.dbPath = dbPath;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            object value = optionsBuilder.UseSqlite($"Data Source={this.dbPath}");
        }
    }
}
