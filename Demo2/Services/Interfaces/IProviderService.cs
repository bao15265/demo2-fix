using Demo2.Entities;

namespace Demo2.Services.Interfaces
{
    public interface IProviderService
    {
        Provider AddProvider(Provider provider);
        Provider UpdateProvider(Provider provider);
        void DeleteProvider(int providerId);
        Provider GetProviderById(int providerId);
    }
}
