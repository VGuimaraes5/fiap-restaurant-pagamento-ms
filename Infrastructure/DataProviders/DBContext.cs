using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Infrastructure.DataProviders.EntityConfigurations;

namespace Infrastructure.DataProviders
{
    public class DBContext : DbContext
    {
        public DbSet<Pagamento> Pagamento { get; set; }

        public DBContext(DbContextOptions<DBContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new PagamentoEntityConfiguration());

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DbContext).Assembly);
        }
    }
}
