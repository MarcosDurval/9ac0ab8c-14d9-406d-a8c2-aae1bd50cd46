using Microsoft.EntityFrameworkCore;
using Orcamentos.Domain;

namespace Orcamentos.Infrastructure;

public sealed class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Orcamento> Orcamentos => Set<Orcamento>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Orcamento>(builder =>
        {
            builder.HasKey(budget => budget.Id);
            builder.Property(budget => budget.ClienteId).IsRequired();
            builder.Property(budget => budget.VeiculoId).IsRequired();
            builder.Ignore(budget => budget.Total);
            builder.Navigation(budget => budget.Itens)
                .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.HasMany(budget => budget.Itens)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<OrcamentoItem>(builder =>
        {
            builder.HasKey(item => item.Id);
            builder.Property(item => item.Descricao).IsRequired();
            builder.Property(item => item.Quantidade).IsRequired();
            builder.Property(item => item.ValorUnitario)
                .HasPrecision(18, 2)
                .IsRequired();
            builder.Ignore(item => item.Subtotal);
        });
    }
}
