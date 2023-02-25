using Company.Delivery.Core;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Company.Delivery.Database;

public class DeliveryDbContext : DbContext
{
    public DeliveryDbContext(DbContextOptions<DeliveryDbContext> options) : base(options) {}

    public DbSet<CargoItem> CargoItems { get; protected init; } = null!;

    public DbSet<Waybill> Waybills { get; protected init; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);

        // TODO: регистрация всех реализаций IEntityTypeConfiguration в сборке Company.Delivery.Database
    }
}