using TiacApp.Models;

namespace TiacApp.Repository.Interface
{
    public interface IPersonRepository
    {
        Task<Person> AddPersonAsync(Person person);
    }
}
