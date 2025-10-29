using TiacApp.Models;

namespace TiacApp.Repository.Interface
{
    public interface IPersonRepository
    {
        Task<Person> AddPersonAsync(Person person);
        Task<List<Person>> GetAllPersonsAsync();
        Task<Person?> GetPersonByIdAsync(int id);
    }
}
