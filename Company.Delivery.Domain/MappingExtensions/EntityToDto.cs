using Company.Delivery.Core;
using Company.Delivery.Domain.Dto;

namespace Company.Delivery.Domain.MappingExtensions;

public static class EntityToDto
{
    public static WaybillDto ToWaybillDto(this Waybill waybill)
    {
        return new WaybillDto()
            {
                Id = waybill.Id,
                Date = waybill.Date,
                Items = waybill.Items?.Select(x => x.ToCargoItemDto()).ToList(),
                Number = waybill.Number
            };
    }

    public static CargoItemDto ToCargoItemDto(this CargoItem cargoItem)
    {
        return new CargoItemDto()
            {
                Id = cargoItem.Id,
                Number = cargoItem.Number,
                Name = cargoItem.Name,
                WaybillId = cargoItem.WaybillId
            };
    }
}