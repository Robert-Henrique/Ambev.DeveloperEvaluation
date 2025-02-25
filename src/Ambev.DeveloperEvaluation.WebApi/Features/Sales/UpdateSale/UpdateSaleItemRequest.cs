using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;

/// <summary>
/// Represents a request to update a sale in the system.
/// </summary>
public class UpdateSaleItemRequest
{
    /// <summary>
    /// The id of the sale item.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The status of the sale item.
    /// </summary>
    public SaleItemStatus Status { get; set; }
}