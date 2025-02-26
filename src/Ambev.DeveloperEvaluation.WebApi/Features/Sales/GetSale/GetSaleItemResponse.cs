using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;

/// <summary>
/// Represents an item in a sale, including the product details, quantity, and pricing information.
/// </summary>
public class GetSaleItemResponse
{
    /// <summary>
    /// The unique identifier of the product in the sale
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// The name or identifier of the product in the sale.
    /// </summary>
    public string ProductName { get; set; } = string.Empty;

    /// <summary>
    /// The quantity of the product purchased in the sale.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// The unit price of the product at the time of the sale.
    /// </summary>
    public decimal UnitPrice { get; set; }

    /// <summary>
    /// The discount applied to the product
    /// </summary>
    public decimal Discount { get; set; }

    /// <summary>
    /// The total price for this item in the sale, accounting for quantity and discount.
    /// </summary>
    public decimal TotalPrice { get; set; }

    /// <summary>
    /// The status of the sale item.
    /// </summary>
    public SaleItemStatus Status { get; set; }
}