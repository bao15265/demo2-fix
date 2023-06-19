using Demo2.DbContexts;
using Demo2.Entities;
using Demo2.Exceptions;

namespace Demo2.Validations
{
    public class StoreValidation
    {
        public static void ValidateStore(Store store, ApplicationDbContext dbContext)
        {
            var existingStore = dbContext.Stores.FirstOrDefault(s => s.Name == store.Name);
            if (existingStore != null)
                throw new DuplicateEntryException("Store with the same name already exists.");
        }
    }

}
