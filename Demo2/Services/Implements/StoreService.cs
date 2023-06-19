using Demo2.DbContexts;
using Demo2.Entities;
using Demo2.Filters;
using Demo2.Services.Interfaces;
using Demo2.Validations;
using System;

namespace Demo2.Services.Implements
{
    public class StoreService : IStoreService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IProviderService _providerService;
        public StoreService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Store AddStore(Store store)
        {
            StoreValidation.ValidateStore(store, _dbContext);
            _dbContext.Stores.Add(store);
            _dbContext.SaveChanges();
            return store;
        }

        public Store UpdateStore(Store store)
        {
            StoreValidation.ValidateStore(store, _dbContext);
            _dbContext.Stores.Update(store);
            _dbContext.SaveChanges();
            return store;
        }

        public void DeleteStore(int storeId)
        {
            var store = _dbContext.Stores.Find(storeId);
            if (store != null)
            {
                _dbContext.Stores.Remove(store);
                _dbContext.SaveChanges();
            }
        }

        public List<Store> GetStores(PaginationFilter filter, string keyword)
        {
            var query = _dbContext.Stores.AsQueryable();
            if (!string.IsNullOrEmpty(keyword))
                query = query.Where(s => s.Name.Contains(keyword) || s.Address.Contains(keyword));

            var stores = query
                .Skip((filter.PageIndex - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToList();

            return stores;
        }

        public List<Provider> GetProvidersWithHighestIntimacy(int storeId)
        {
            var providers = _dbContext.StoreProviders
                .Where(sp => sp.StoreId == storeId)
                .GroupBy(sp => new { sp.ProviderId })
                .OrderByDescending(g => g.Max(sp => sp.IntimacyLevel))
                .Select(g => _providerService.GetProviderById(g.Key.ProviderId))
                .ToList();

            return providers;
        }
    }

}
