using AutoMapper;
using Company.Delivery.Api.Controllers.Waybills.Response;
using Company.Delivery.Domain.Dto;

namespace Company.Delivery.Api.AutoMapperProfiles;

/// <summary>
/// WaybillProfile
/// </summary>
public class WaybillProfile : Profile
{
    /// <summary>
    /// Constructor
    /// </summary>
    public WaybillProfile()
    {
        CreateMap<WaybillDto, WaybillResponse>();
    }
}