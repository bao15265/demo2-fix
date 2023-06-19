using Demo2.DbContexts;
using Demo2.Dto.Provider;
using Demo2.Entities;
using Demo2.Exceptions;
using Demo2.Services.Interfaces;

namespace Demo2.Services.Implements
{
    public class ProviderService : IProviderService
    {
        private readonly ApplicationDbContext _dbContext;

        public ProviderService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ProviderDto AddProvider(ProviderDto providerDto)
        {
            if (_dbContext.Providers.Any(p => p.Name == providerDto.Name))
            {
                throw new DuplicateEntityException("A provider with the same name already exists.");
            }

            var provider = new Provider
            {
                Name = providerDto.Name,
                PhoneNumber = providerDto.PhoneNumber
            };

            _dbContext.Providers.Add(provider);
            _dbContext.SaveChanges();

            return new ProviderDto
            {
                Name = provider.Name,
                PhoneNumber = provider.PhoneNumber
            };
        }

        public ProviderDto UpdateProvider(int id, ProviderDto providerDto)
        {
            var provider = _dbContext.Providers.Find(id);
            if (provider == null)
            {
                throw new EntityNotFoundException("Provider not found.");
            }

            if (_dbContext.Providers.Any(p => p.Name == providerDto.Name && p.Id != id))
            {
                throw new DuplicateEntityException("A provider with the same name already exists.");
            }

            provider.Name = providerDto.Name;
            provider.PhoneNumber = providerDto.PhoneNumber;

            _dbContext.SaveChanges();

            return new ProviderDto
            {
                Name = provider.Name,
                PhoneNumber = provider.PhoneNumber
            };
        }

        public void DeleteProvider(int id)
        {
            var provider = _dbContext.Providers.Find(id);
            if (provider == null)
            {
                throw new EntityNotFoundException("Provider not found.");
            }

            _dbContext.Providers.Remove(provider);
            _dbContext.SaveChanges();
        }

        public ProviderDto GetProviderById(int id)
        {
            var provider = _dbContext.Providers.Find(id);
            if (provider == null)
            {
                throw new EntityNotFoundException("Provider not found.");
            }

            return new ProviderDto
            {
                Name = provider.Name,
                PhoneNumber = provider.PhoneNumber
            };
        }
    }
}
