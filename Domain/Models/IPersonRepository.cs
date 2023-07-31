namespace Domain.Models
{
    public interface IPersonRepository
    {
        Task<List<Person>> FindAllAsync(int pageNumber, int pageQuantity);
        Task<Person?> FindByIdAsync(int id);
        Task<List<Person>> FindByUFAsync(string uf, int pageNumber, int pageQuantity);

        Task<Person?> AddAsync(Person person);
        Task<Person?> UpdateAsync(int id, Person person);
        Task<bool> DeleteAsync(int id);
    }
}