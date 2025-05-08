using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI_6_01.Domain;
using WebAPI_6_01.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace TestProject1
{
    public class TestHelper
    {
        private readonly Context _context;
        public TestHelper()
        {
            //Используем базу обычную базу данных, не в памяти
            //Имя тестовой базы данных должно отличатсья от базы данных проекта
            var contextOptions = new DbContextOptionsBuilder<Context>()
                .UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Test")
                .Options;

            _context = new Context(contextOptions);


            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            //Значение идентификатора явно не задаем. Используем для идентификации уникальное в рамках теста имя
            var person1 = new Person
            {
                Name = "Nic",
            };
            person1.AddPhone(new Phone { PhoneType = "Work", PhoneNumber = "111-111" });
            person1.AddPhone(new Phone { PhoneType = "Home", PhoneNumber = "222-222" });

            _context.Persons.Add(person1);
            _context.SaveChanges();
            //Запрещаем отслеживание (разрываем связи с БД)
            _context.ChangeTracker.Clear();
        }

        public PersonRepository PersonRepository
        {
            get
            {
                return new PersonRepository(_context);
            }
        }
    }
}
