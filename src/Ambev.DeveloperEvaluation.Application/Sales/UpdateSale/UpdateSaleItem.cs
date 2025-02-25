using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

public class UpdateSaleItem
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