using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

namespace Infrastructure.DataProviders.EntityConfigurations
{
    public class PedidoEntityConfiguration : IEntityTypeConfiguration<Pedido>
    {
        public void Configure(EntityTypeBuilder<Pedido> builder)
        {
            builder
                .ToTable("tb_Pedido")
                .HasKey(p => new { p.Id });

            builder.Property(p => p.Senha)
                .HasColumnType("int")
                .UseMySqlIdentityColumn();

            builder.HasIndex(p => p.Senha)
                .IsUnique();

            builder.Property(p => p.Status)
                .HasColumnType("int")
                .IsRequired(true)
                .HasConversion<int>();

            builder.HasMany(p => p.ItensPedido)
                .WithOne(p => p.Pedido)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
