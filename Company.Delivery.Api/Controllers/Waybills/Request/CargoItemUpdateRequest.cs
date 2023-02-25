namespace Company.Delivery.Api.Controllers.Waybills.Request;

/// <summary>
/// Cargo item
/// </summary>
public class CargoItemUpdateRequest
{
    // Возможно стоит добавить Id для удобства в методе update, но не уверен требуется ли это по задаче, поэтому не добавил
    /// <summary>
    /// Number
    /// </summary>
    public string Number { get; init; } = null!;

    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; init; } = null!;
}