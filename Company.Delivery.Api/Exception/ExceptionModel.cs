namespace Company.Delivery.Api.Exception;

/// <summary>
/// ExceptionModel
/// </summary>
public class ExceptionModel
{
    /// <summary>
    /// StatusCode
    /// </summary>
    public int StatusCode { get; set; }
    /// <summary>
    /// Message
    /// </summary>
    public string? Message { get; set; }
    /// <summary>
    /// Content
    /// </summary>
    public string? Content { get; set; }
}