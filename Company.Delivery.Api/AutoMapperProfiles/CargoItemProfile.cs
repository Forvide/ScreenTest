using AutoMapper;
using Company.Delivery.Api.Controllers.Waybills.Response;
using Company.Delivery.Domain.Dto;

namespace Company.Delivery.Api.AutoMapperProfiles;

/// <summary>
/// CargoItemProfile
/// </summary>
public class CargoItemProfile : Profile
{
    /// <summary>
    /// CargoItemProfile
    /// </summary>
    public CargoItemProfile()
    {
        // Следует удалить, но оставил в демонстрационных целях
        CreateMap<CargoItemDto, CargoItemResponse>();
    }
}