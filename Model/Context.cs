using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffManager.Model
{
    public class Context : DbContext
    {

        public DbSet<Person> Persons { get; set; } // прописываем каждый класс для связи
        public DbSet<Department> Departments { get; set; } 
        public DbSet<Position> Positions { get; set; }
        public DbSet<SpecialWork> SpWorks { get; set; }

        public Context()
        {
            Database.EnsureCreated(); // создает БД
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseNpgsql("Host=localhost;Port=5433;Database=StaffManager;Username=postgres;Password=User1234")
                //.LogTo(Console.WriteLine) // можно подсмотреть сформированный EF запрос
                ;
            /*Строка подключения содержит адрес сервера (параметр Host), порт (Port), 
             * название базы данных на сервере (StaffManager),
             * имя пользователя в рамках сервера PostgreSQL (Username) и его пароль (Password).*/
        }

    }
}
