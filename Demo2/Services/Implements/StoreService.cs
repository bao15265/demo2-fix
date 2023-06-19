using Demo2.DbContexts;
using Demo2.Dto.Provider;
using Demo2.Dto.Store;
using Demo2.Entities;
using Demo2.Exceptions;
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

        public StoreDto AddStore(StoreDto storeDto)
        {
            if (_dbContext.Stores.Any(s => s.Name == storeDto.Name))
            {
                //throw new DuplicateEntityException("A store with the same name already exists.");
            }

            var store = new Store
            {
                Name = storeDto.Name,
                Address = storeDto.Address,
                OpenTime = storeDto.OpenTime,
                CloseTime = storeDto.CloseTime
            };

            _dbContext.Stores.Add(store);
            _dbContext.SaveChanges();

            return new StoreDto
            {
                Name = store.Name,
                Address = store.Address,
                OpenTime = store.OpenTime,
                CloseTime = store.CloseTime
            };
        }

        public StoreDto UpdateStore(int id, StoreDto storeDto)
        {
            var store = _dbContext.Stores.Find(id);
            if (store == null)
            {
                throw new EntityNotFoundException("Store not found.");
            }

            if (_dbContext.Stores.Any(s => s.Name == storeDto.Name && s.Id != id))
            {
                throw new DuplicateEntityException("A store with the same name already exists.");
            }

            store.Name = storeDto.Name;
            store.Address = storeDto.Address;
            store.OpenTime = storeDto.OpenTime;
            store.CloseTime = storeDto.CloseTime;

            _dbContext.SaveChanges();

            return new StoreDto
            {
                Name = store.Name,
                Address = store.Address,
                OpenTime = store.OpenTime,
                CloseTime = store.CloseTime
            };
        }

        public void DeleteStore(int id)
        {
            var store = _dbContext.Stores.Find(id);
            if (store == null)
            {
                throw new EntityNotFoundException("Store not found.");
            }

            _dbContext.Stores.Remove(store);
            _dbContext.SaveChanges();
        }

        public List<StoreDto> SearchStores(StoreFilterDto filter)
        {
            var query = _dbContext.Stores.AsQueryable();

            if (!string.IsNullOrEmpty(filter.Keyword))
            {
                query = query.Where(s => s.Name.Contains(filter.Keyword) || s.Address.Contains(filter.Keyword));
            }

            var stores = query
                .OrderBy(s => s.Name)
                .Skip(filter.PageSize * (filter.PageIndex - 1))
                .Take(filter.PageSize)
                .Select(s => new StoreDto
                {
                    Name = s.Name,
                    Address = s.Address,
                    OpenTime = s.OpenTime,
                    CloseTime = s.CloseTime
                })
                .ToList();

            return stores;
        }

        public List<ProviderDto> GetTopProviders(int storeId)
        {
            var store = _dbContext.Stores.Find(storeId);
            if (store == null)
            {
                throw new EntityNotFoundException("Store not found.");
            }

            var providers = _dbContext.StoreProviders
                .Where(sp => sp.StoreId == storeId)
                .OrderByDescending(sp => sp.IntimacyLevel)
                .Select(sp => _providerService.GetProviderById(sp.StoreId))
                .ToList();

            return providers;
        }
    }
}
