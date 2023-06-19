using Demo2.Dto.Provider;
using Demo2.Dto.Store;
using Demo2.Entities;
using Demo2.Filters;

namespace Demo2.Services.Interfaces
{
    public interface IStoreService
    {
        StoreDto AddStore(StoreDto storeDto);
        StoreDto UpdateStore(int id, StoreDto storeDto);
        void DeleteStore(int id);
        List<StoreDto> SearchStores(StoreFilterDto filter);
        List<ProviderDto> GetTopProviders(int storeId);
    }
}
