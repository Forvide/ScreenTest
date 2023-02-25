using Company.Delivery.Core;
using Company.Delivery.Database;
using Company.Delivery.Domain;
using Company.Delivery.Domain.Dto;
using Company.Delivery.Domain.MappingExtensions;
using Microsoft.EntityFrameworkCore;

namespace Company.Delivery.Infrastructure;

public class WaybillService : IWaybillService
{
    private readonly DeliveryDbContext _dbContext;

    public WaybillService(DeliveryDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<WaybillDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        // TODO: Если сущность не найдена по идентификатору, кинуть исключение типа EntityNotFoundException
        var waybill = await _dbContext.Waybills.Include(x => x.Items)
                                               .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (waybill is null)
        {
            throw new EntityNotFoundException();
        }

        return waybill.ToWaybillDto();
    }

    public async Task<WaybillDto> CreateAsync(WaybillCreateDto data, CancellationToken cancellationToken)
    {
        var waybill = data.ToWaybillFromDto();
        await _dbContext.Waybills.AddAsync(waybill, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return waybill.ToWaybillDto();
    }

    public async Task<WaybillDto> UpdateByIdAsync(Guid id, WaybillUpdateDto data, CancellationToken cancellationToken)
    {
        // TODO: Если сущность не найдена по идентификатору, кинуть исключение типа EntityNotFoundException
        var waybill = await _dbContext.Waybills.Include(x => x.Items)
                                               .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (waybill is null)
        {
            throw new EntityNotFoundException();
        }

        IList<CargoItem>? cargoItems;
        if (waybill.Items is null)
        {
            cargoItems = null;
        }
        else
        {
            var existingCargoItemsNumbers = waybill.Items.Select(x => x.Number).ToArray();

            cargoItems = data.Items?.Select(x => existingCargoItemsNumbers.Contains(x.Number)
                ? waybill.Items.Single(y => y.Number == x.Number)
                : x.ToCargoItemFromDto(waybill.Id)).ToList();
        }
        waybill.Date = data.Date;
        waybill.Items = cargoItems;
        waybill.Number = data.Number;
        await _dbContext.SaveChangesAsync(cancellationToken);

        return waybill.ToWaybillDto();
    }

    public async Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        // TODO: Если сущность не найдена по идентификатору, кинуть исключение типа EntityNotFoundException
        var waybill = await _dbContext.Waybills.FindAsync(new object?[] { id }, cancellationToken);

        if (waybill is null)
        {
            throw new EntityNotFoundException();
        }

        _dbContext.Waybills.Remove(waybill);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}