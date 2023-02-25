using Company.Delivery.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Company.Delivery.Database.ModelConfigurations;

internal class WaybillConfiguration : IEntityTypeConfiguration<Waybill>
{
    public void Configure(EntityTypeBuilder<Waybill> builder)
    {
        builder.HasIndex(x => x.Number).IsUnique();
        builder.Property(x => x.Number).HasMaxLength(20);
        // TODO: все строковые свойства должны иметь ограничение на длину
        // TODO: должно быть ограничение на уникальность свойства Waybill.Number
        // TODO: ApplicationDbContextTests должен выполняться без ошибок
    }
}