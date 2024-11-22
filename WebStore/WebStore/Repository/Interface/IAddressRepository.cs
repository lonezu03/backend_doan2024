using WebStore.Entity;

namespace WebStore.Repository.Interface
{
    public interface IAddressRepository
    {
        Task<List<Address>> GetAllAsync();
        Task<Address> GetByIdAsync(int id);
        Task AddAsync(Address address);
        Task DeleteByIdAsync(int id);
        Task SaveChangesAsync();
    }

}
