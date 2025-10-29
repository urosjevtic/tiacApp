using Microsoft.EntityFrameworkCore;
using TiacApp.Infrastructure;
using TiacApp.Models;
using TiacApp.Repository.Interface;

namespace TiacApp.Repository
{
    public class PersonRepository : IPersonRepository
    {
        private AppDbContext _context;

        public PersonRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Person> AddPersonAsync(Person person)
        {
            _context.Person.Add(person);
            await _context.SaveChangesAsync();
            return person;
        }

        public async Task<List<Person>> GetAllPersonsAsync()
        {
            return await _context.Person
                .Include(p => p.SocialMedias)
                .ToListAsync();
        }

        public async Task<Person?> GetPersonByIdAsync(int id)
        {
            return await _context.Person
                .Include(p => p.SocialMedias)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
