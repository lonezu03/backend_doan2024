using WebStore.Entity;

namespace WebStore.Service.IService
{
    public interface IAddressService
    {
        Task<List<Address>> GetAllAsync();
        Task<Address> GetByIdAsync(int id);
        Task AddAsync(Address address);
        Task DeleteByIdAsync(int id);
    }

}
