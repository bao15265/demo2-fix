using Demo2.Entities;
using Demo2.Filters;

namespace Demo2.Services.Interfaces
{
    public interface IStoreService
    {
        Store AddStore(Store store);
        Store UpdateStore(Store store);
        void DeleteStore(int storeId);
        List<Store> GetStores(PaginationFilter filter, string keyword);
        List<Provider> GetProvidersWithHighestIntimacy(int storeId);
    }

}
