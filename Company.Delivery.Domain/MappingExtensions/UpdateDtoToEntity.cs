using Company.Delivery.Core;
using Company.Delivery.Domain.Dto;

namespace Company.Delivery.Domain.MappingExtensions;

public static class UpdateDtoToEntity
{
    public static CargoItem ToCargoItemFromDto(this CargoItemUpdateDto dto, Guid waybillId)
    {
        return new CargoItem()
        {
            Number = dto.Number,
            Name = dto.Name,
            WaybillId = waybillId
        };
    }
}