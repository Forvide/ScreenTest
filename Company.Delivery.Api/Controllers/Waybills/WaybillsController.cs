using Company.Delivery.Api.Controllers.Waybills.Request;
using Company.Delivery.Api.Controllers.Waybills.Response;
using Company.Delivery.Domain;
using Company.Delivery.Domain.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Company.Delivery.Api.Controllers.Waybills;

/// <summary>
/// Waybills management
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class WaybillsController : ControllerBase
{
    private readonly IWaybillService _waybillService;

    /// <summary>
    /// Waybills management
    /// </summary>
    public WaybillsController(IWaybillService waybillService)
    {
        _waybillService = waybillService;
    }

    /// <summary>
    /// Получение Waybill
    /// </summary>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(WaybillResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        // TODO: вернуть ответ с кодом 200 если найдено или кодом 404 если не найдено
        // TODO: WaybillsControllerTests должен выполняться без ошибок
        try
        {
            // Примечания:
            // 1. Реализован ExceptionMiddleware, но тесты по ним не проходят
            // 2. Возможно использование AutoMapper, но нужно изменение тестов
            var waybill = await _waybillService.GetByIdAsync(id, cancellationToken);
            var response = new WaybillResponse
            {
                Id = waybill.Id,
                Date = waybill.Date,
                Items = waybill.Items?.Select(x => new CargoItemResponse
                {
                    Id = x.Id,
                    Number = x.Number,
                    Name = x.Name,
                    WaybillId = x.WaybillId
                }),
                Number = waybill.Number
            };
            return Ok(response);
        }
        catch (System.Exception e)
        {
            if (e is EntityNotFoundException)
            {
                return NotFound();
            }
            throw;
        }
    }

    /// <summary>
    /// Создание Waybill
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(WaybillResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateAsync([FromBody] WaybillCreateRequest request, CancellationToken cancellationToken)
    {
        // TODO: вернуть ответ с кодом 200 если успешно создано
        // TODO: WaybillsControllerTests должен выполняться без ошибок
        // Примечания:
        // 1. Возможно использование AutoMapper, но нужно изменение тестов
        var dto = new WaybillCreateDto
        {
            Number = request.Number,
            Date = request.Date,
            Items = request.Items?.Select(x => new CargoItemCreateDto
            {
                Number = x.Number,
                Name = x.Name
            })
        };
        var waybill = await _waybillService.CreateAsync(dto, cancellationToken);
        var response = new WaybillResponse
        {
            Id = waybill.Id,
            Date = waybill.Date,
            Items = waybill.Items?.Select(x => new CargoItemResponse
            {
                Id = x.Id,
                Number = x.Number,
                Name = x.Name,
                WaybillId = x.WaybillId
            }),
            Number = waybill.Number
        };
        return Ok(response);
    }

    /// <summary>
    /// Редактирование Waybill
    /// </summary>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(WaybillResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateByIdAsync(Guid id, [FromBody] WaybillUpdateRequest request, CancellationToken cancellationToken)
    {
        // TODO: вернуть ответ с кодом 200 если найдено и изменено, или 404 если не найдено
        // TODO: WaybillsControllerTests должен выполняться без ошибок
        // Примечания:
        // 1. Реализован ExceptionMiddleware, но тесты по ним не проходят
        // 2. Возможно использование AutoMapper, но нужно изменение тестов
        var updateDto = new WaybillUpdateDto
        {
            Date = request.Date,
            Number = request.Number,
            Items = request.Items?.Select(x => new CargoItemUpdateDto
            {
                Name = x.Name,
                Number = x.Number
            })
        };
        try
        {
            var updatedWaybill = await _waybillService.UpdateByIdAsync(id, updateDto, cancellationToken);
            var response = new WaybillResponse
            {
                Id = updatedWaybill.Id,
                Date = updatedWaybill.Date,
                Items = updatedWaybill.Items?.Select(x => new CargoItemResponse
                {
                    Id = x.Id,
                    Number = x.Number,
                    Name = x.Name,
                    WaybillId = x.WaybillId
                }),
                Number = updatedWaybill.Number
            };
            return Ok(response);
        }
        catch (System.Exception e)
        {
            if (e is EntityNotFoundException)
            {
                return NotFound();
            }
            throw;
        }
    }

    /// <summary>
    /// Удаление Waybill
    /// </summary>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        // TODO: вернуть ответ с кодом 200 если найдено и удалено, или 404 если не найдено
        // TODO: WaybillsControllerTests должен выполняться без ошибок
        // Примечания:
        // 1. Реализован ExceptionMiddleware, но тесты по ним не проходят
        // 2. Возможно использование AutoMapper, но нужно изменение тестов
        try
        {
            await _waybillService.DeleteByIdAsync(id, cancellationToken);
            return Ok();
        }
        catch (System.Exception e)
        {
            if (e is EntityNotFoundException)
            {
                return NotFound();
            }
            throw;
        }
    }
}