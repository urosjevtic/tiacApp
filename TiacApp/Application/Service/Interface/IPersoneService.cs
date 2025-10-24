namespace TiacApp.Application.Service.Interface
{
    public interface IPersoneService
    {
        Task<object> GetPersons();
        Task<object> GetPersonById(int id);
        Task<object> AddPerson(object persone);
    }
}
