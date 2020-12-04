using Customer.Domain;
using Customer.Persistence.Database.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Customer.Persistence.Database
{
    public class CustomerDbContext : DbContext
    {
        public CustomerDbContext(
            DbContextOptions<CustomerDbContext> options
        )
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Database schema
            builder.HasDefaultSchema("Customer");

            // Model Contraints
            ModelConfig(builder);
        }

        public DbSet<Client> Clients { get; set; }

        private void ModelConfig(ModelBuilder modelBuilder)
        {
            new ClientConfiguration(modelBuilder.Entity<Client>());
        }

        //add-migration InitialCreate
        //update-database -context CustomerDbContext
    }
}
