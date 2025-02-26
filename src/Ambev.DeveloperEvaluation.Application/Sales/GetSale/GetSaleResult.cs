using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale;

/// <summary>
/// API response model for GetSale operation
/// </summary>
public class GetSaleResult
{
    /// <summary>
    /// The unique identifier of the sale (Sale Number)
    /// </summary>
    public string Number { get; set; } = string.Empty;

    /// <summary>
    /// The date when the sale was made
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// The unique identifier of the purchasing customer
    /// </summary>
    public Guid CustomerId { get; set; }

    /// <summary>
    /// The name of the purchasing customer
    /// </summary>
    public string CustomerName { get; set; } = string.Empty;

    /// <summary>
    /// The total amount for the sale
    /// </summary>
    public decimal TotalAmount { get; set; }

    /// <summary>
    /// The unique identifier of the branch where the sale occurred
    /// </summary>
    public Guid BranchId { get; set; }

    /// <summary>
    /// The name of the branch where the sale occurred
    /// </summary>
    public string BranchName { get; set; } = string.Empty;

    /// <summary>
    /// The list of items involved in the sale
    /// </summary>
    public IList<GetSaleItemResult> Items { get; set; } = new List<GetSaleItemResult>(0);

    /// <summary>
    /// The current status of the sale
    /// </summary>
    public SaleStatus Status { get; set; }
}