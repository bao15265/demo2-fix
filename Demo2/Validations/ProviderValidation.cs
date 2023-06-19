using Demo2.DbContexts;
using Demo2.Entities;
using Demo2.Exceptions;

namespace Demo2.Validations
{
    public class ProviderValidation
    {
        public static void ValidateProvider(Provider provider, ApplicationDbContext dbContext)
        {
            var existingProvider = dbContext.Providers.FirstOrDefault(p => p.Name == provider.Name);
            if (existingProvider != null)
                throw new DuplicateEntryException("Provider with the same name already exists.");
        }
    }

}
