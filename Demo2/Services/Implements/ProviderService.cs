using Demo2.DbContexts;
using Demo2.Entities;
using Demo2.Services.Interfaces;
using Demo2.Validations;

namespace Demo2.Services.Implements
{
    public class ProviderService : IProviderService
    {
        private readonly ApplicationDbContext _dbContext;

        public ProviderService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Provider AddProvider(Provider provider)
        {
            ProviderValidation.ValidateProvider(provider, _dbContext);
            _dbContext.Providers.Add(provider);
            _dbContext.SaveChanges();
            return provider;
        }

        public Provider UpdateProvider(Provider provider)
        {
            ProviderValidation.ValidateProvider(provider, _dbContext);
            _dbContext.Providers.Update(provider);
            _dbContext.SaveChanges();
            return provider;
        }

        public void DeleteProvider(int providerId)
        {
            var provider = _dbContext.Providers.Find(providerId);
            if (provider != null)
            {
                _dbContext.Providers.Remove(provider);
                _dbContext.SaveChanges();
            }
        }

        public Provider GetProviderById(int providerId)
        {
            return _dbContext.Providers.Find(providerId);
        }
    }
}
