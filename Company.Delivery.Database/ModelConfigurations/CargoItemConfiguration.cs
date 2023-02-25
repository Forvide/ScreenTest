using Company.Delivery.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Company.Delivery.Database.ModelConfigurations;

internal class CargoItemConfiguration : IEntityTypeConfiguration<CargoItem>
{
    public void Configure(EntityTypeBuilder<CargoItem> builder)
    {
        // По моему мнению, возможно проверки на уникальность стоит реализовать на уровне приложения,
        // ограничив тем самым зону ответственности слоя работы с базой данных.
        // И в указанном случае можно реализовать более удобную обработку ошибок

        // При update с одинаковыми номерами не производит вставку, но и не отправляет ошибку
        builder.HasIndex("Number", "WaybillId").IsUnique();
        builder.Property(x => x.Number).HasMaxLength(20);
        builder.Property(x => x.Name).HasMaxLength(100);
        // TODO: все строковые свойства должны иметь ограничение на длину
        // TODO: должно быть ограничение на уникальность свойства CargoItem.Number в пределах одной сущности Waybill
        // TODO: ApplicationDbContextTests должен выполняться без ошибок
    }
}