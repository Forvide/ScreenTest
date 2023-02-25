using Company.Delivery.Core;
using Company.Delivery.Domain.Dto;

namespace Company.Delivery.Domain.MappingExtensions;

public static class CreateDtoToEntity
{
    public static Waybill ToWaybillFromDto(this WaybillCreateDto dto)
    {
        var newGuid = Guid.NewGuid();
        return new Waybill()
        {
            Id = newGuid,
            Date = dto.Date,
            Items = dto.Items?.Select(x => x.ToCargoItemFromDto(newGuid)).ToList(),
            Number = dto.Number
        };
    }

    public static CargoItem ToCargoItemFromDto(this CargoItemCreateDto dto, Guid waybillId)
    {
        return new CargoItem()
        {
            Number = dto.Number,
            Name = dto.Name,
            WaybillId = waybillId
        };
    }
}