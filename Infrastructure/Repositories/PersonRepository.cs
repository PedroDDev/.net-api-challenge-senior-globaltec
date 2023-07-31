using Domain.DTOs;
using Domain.Models;

namespace Infrastructure.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly DataContext _context;

        public PersonRepository(DataContext context) => _context = context;

        public async Task<Person?> AddAsync(Person person)
        {
            if (!(await _context.People.AnyAsync(p => p.Cpf == person.Cpf)))
            {
                await _context.People.AddAsync(person);
                await _context.SaveChangesAsync();

                return person;
            }

            return null;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            Person? person = await _context.People.FindAsync(id);
            if (person is not null)
            {
                _context.People.Remove(person);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        
        public async Task<List<Person>> FindAllAsync(int pageNumber, int pageQuantity)
        {
            return await _context.People.Skip(pageNumber * pageQuantity).Take(pageQuantity).ToListAsync();
        }

        public async Task<Person?> FindByIdAsync(int id)
        {
            return await _context.People.FindAsync(id);
        }

        public async Task<List<Person>> FindByUFAsync(string uf, int pageNumber, int pageQuantity)
        {
            return await _context.People.Skip(pageNumber * pageQuantity).Take(pageQuantity).Where(p => p.Uf == uf).ToListAsync();
        }

        
        public async Task<Person?> UpdateAsync(int id, Person person)
        {
            Person? dbPerson = await _context.People.FindAsync(id);

            if (dbPerson is not null)
            {
                dbPerson.UpdatePerson(person.Name, person.Cpf, person.Uf, person.Birthdate);
                await _context.SaveChangesAsync();
            }

            return dbPerson;
        }
    }
}