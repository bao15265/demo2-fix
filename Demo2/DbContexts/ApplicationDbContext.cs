using Microsoft.EntityFrameworkCore;
using Demo2.Entities;

namespace Demo2.DbContexts
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Store> Stores { get; set; }
        public DbSet<Provider> Providers { get; set; }
        public DbSet<StoreProvider> StoreProviders { get; set; }
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StoreProvider>()
              .HasOne<Store>()
              .WithMany()
                .HasForeignKey(e => e.StoreId);

            modelBuilder.Entity<StoreProvider>()
                  .HasOne<Provider>()
                  .WithMany()
                  .HasForeignKey(e => e.ProviderId);
        }
    }
}
