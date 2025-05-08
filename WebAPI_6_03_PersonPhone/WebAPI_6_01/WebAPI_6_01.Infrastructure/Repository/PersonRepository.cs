using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI_6_01.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace WebAPI_6_01.Infrastructure
{
    public class PersonRepository
    {
        private readonly Context _context;
        public Context UnitOfWork
        {
            get
            {
                return _context;
            }
        }
        public PersonRepository(Context context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<List<Person>> GetAllAsync()
        {
            return await _context.Persons.Include(p => p.Phones).OrderBy(p => p.Name).ToListAsync();
        }
        public async Task<Person> GetByIdAsync(Guid id)
        {
            return await _context.Persons.Where(p => p.Id == id)
                .OrderBy(p => p.Name)
                .Include(p => p.Phones)
                .FirstOrDefaultAsync();
        }

        public async Task<Person> GetByNameAsync(string name)
        {
            return await _context.Persons
                .Where(p => p.Name == name)
                .Include(p => p.Phones)
                .FirstOrDefaultAsync();
        }

        public async Task<Person> AddAsync(Person person)
        {
            _context.Persons.Add(person);
            await _context.SaveChangesAsync();
            return person;
        }
        public async Task UpdateAsync(Person person)
        {
            var existPerson = GetByIdAsync(person.Id).Result;
            if(existPerson != null)
            {
                _context.Entry(existPerson).CurrentValues.SetValues(person);
                foreach(var phoneNumber in person.Phones)
                {
                    var existPoneNumber = existPerson.Phones.FirstOrDefault(pn => pn.Id == phoneNumber.Id);
                    if (existPoneNumber == null)
                    {
                        existPerson.Phones.Add(phoneNumber);
                    }
                    else
                    {
                        _context.Entry(existPoneNumber).CurrentValues.SetValues(phoneNumber);
                    }
                }
                foreach(var existNumber in existPerson.Phones)
                { 
                    if(!person.Phones.Any(pn => pn.Id == existNumber.Id))
                    {
                        _context.Remove(existNumber);
                    }
                }
            }

            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(Guid id)
        {
            Person person = await _context.Persons.FindAsync(id);
            _context.Remove(person);
            await _context.SaveChangesAsync();
        }

        public void ChangeTrackerClear()
        {
            _context.ChangeTracker.Clear();
        }
    }
}
