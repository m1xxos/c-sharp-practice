using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using WebAPI_6_01.Domain;

namespace TestProject1
{
    public class TestPersonRepository
    {
        [Fact]
        //Тест, проверяющий, что база данных создалась
        public void VoidTest()
        {
            var testHelper = new TestHelper();
            var personRepository = testHelper.PersonRepository;

            Assert.Equal(1, 1);
        }

        [Fact]
        public async void TestAdd()
        {
            var testHelper = new TestHelper();
            var personRepository = testHelper.PersonRepository;
            var person = new Person { Name = "Ivan" };
            person.Id = Guid.NewGuid();

            var perons = await personRepository.AddAsync(person);
            //Запрещаем отслеживание сущностей (разрываем связи с БД)
            personRepository.ChangeTrackerClear();

            Assert.True(personRepository.GetAllAsync().Result.Count == 2);
            Assert.Equal("Ivan", personRepository.GetByIdAsync(person.Id).Result.Name);
            Assert.Equal("Nic", personRepository.GetByNameAsync("Nic").Result.Name);
            Assert.Equal("Ivan", personRepository.GetByNameAsync("Ivan").Result.Name);
            Assert.Equal(2, personRepository.GetByNameAsync("Nic").Result.PhoneCount);
        }

        [Fact]
        public void TestUpdateAdd()
        {
            var testHelper = new TestHelper();
            var personRepository = testHelper.PersonRepository;
            var person = personRepository.GetByNameAsync("Nic").Result;
            //Запрещаем отслеживание сущностей (разрываем связи с БД)
            personRepository.ChangeTrackerClear();
            person.Name = "Ivanov Ivan";
            var phoneNumber = new Phone { PhoneType = "Mobile", PhoneNumber = "333-333" };
            person.AddPhone(phoneNumber);

            personRepository.UpdateAsync(person).Wait();

            Assert.Equal("Ivanov Ivan", personRepository.GetByNameAsync("Ivanov Ivan").Result.Name);
            Assert.Equal(3, personRepository.GetByNameAsync("Ivanov Ivan").Result.PhoneCount);
        }

        [Fact]
        public void TestUpdateDelete()
        {
            var testHelper = new TestHelper();
            var personRepository = testHelper.PersonRepository;
            var person = personRepository.GetByNameAsync("Nic").Result;
            //Запрещаем отслеживание сущностей (разрываем связи с БД)
            personRepository.ChangeTrackerClear();
            person.RemoveAt(0);

            personRepository.UpdateAsync(person).Wait();

            Assert.Equal(1, personRepository.GetByNameAsync("Nic").Result.PhoneCount);
        }
    }
}
