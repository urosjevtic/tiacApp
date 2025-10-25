using Microsoft.EntityFrameworkCore;
using TiacApp.Infrastructure;
using TiacApp.Models;

namespace TiacApp.Repository
{
    public class PersonRepository
    {
        private AppDbContext _context;

        public PersonRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Models.Person> AddPersonAsync(Person person)
        {
            _context.Persons.Add(person);
            await _context.SaveChangesAsync();
            return person;
        }

        public async Task<List<Models.Person>> GetAllPersonsAsync()
        {
            return await _context.Persons.ToListAsync();
        }

        public async Task<Models.Person?> GetPersonByIdAsync(int id)
        {
            return await _context.Persons.FindAsync(id);
        }
    }
}
