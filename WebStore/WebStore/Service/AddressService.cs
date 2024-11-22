using WebStore.Entity;
using WebStore.Repository.Interface;
using WebStore.Service.IService;

namespace WebStore.Service
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _addressRepository;

        public AddressService(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }

        public async Task<List<Address>> GetAllAsync()
        {
            return await _addressRepository.GetAllAsync();
        }

        public async Task<Address> GetByIdAsync(int id)
        {
            return await _addressRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Address address)
        {
            await _addressRepository.AddAsync(address);
            await _addressRepository.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            await _addressRepository.DeleteByIdAsync(id);
            await _addressRepository.SaveChangesAsync();
        }
    }

}
