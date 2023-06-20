using Demo2.Dto.Provider;
using Demo2.Entities;

namespace Demo2.Services.Interfaces
{
    public interface IProviderService
    {
        void AddProvider(ProviderDto providerDto);
        void UpdateProvider(int id, ProviderDto providerDto);
        void DeleteProvider(int id);
        ProviderDto GetProviderById(int id);
    }
}
